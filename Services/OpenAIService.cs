using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using HelpdeskAPI.Models;
using Microsoft.Extensions.Configuration;
using HelpdeskAPI.Data;

namespace HelpdeskAPI.Services;

public class OpenAIService
{
private readonly HttpClient _httpClient;
private readonly IConfiguration _configuration;
private readonly AppDbContext _context;

  public OpenAIService(
    HttpClient httpClient,
    IConfiguration configuration,
    AppDbContext context)
{
    _httpClient = httpClient;
    _configuration = configuration;
    _context = context;
}

public async Task<AIAssignment> AnalyzeTicket(string title, string description)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        var model = _configuration["OpenAI:Model"];


        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                apiKey);


var engineers = _context.Engineers.ToList();

var engineerList = new StringBuilder();

foreach (var engineer in engineers)
{
    var openTickets = _context.Tickets.Count(t =>
        t.AssignedEngineer == engineer.Name &&
        t.Status != "Closed");

    engineerList.AppendLine($"Name: {engineer.Name}");
    engineerList.AppendLine($"Skills: {engineer.Skills}");
    engineerList.AppendLine($"Open Tickets: {openTickets}");
    engineerList.AppendLine();
}

var prompt = $@"
You are a helpdesk ticket assignment AI.

Analyze this ticket.

Title:
{title}

Description:
{description}

Return ONLY valid JSON.

{{
  ""category"": """",
  ""priority"": """",
  ""assignedEngineer"": """",
  ""reason"": """"
}}

Available Engineers

{engineerList}

Assign the ticket to the engineer who:

1. Has the required skills.
2. Has the fewest open tickets.
3. If multiple engineers qualify, choose the one with the lowest workload.

I want you to ensure equitable assignment. Make sure to not consistently assign to one engineer.

Return ONLY valid JSON.
";

        var requestBody = new
        {
            model = model,
            input = prompt
        };


        var json = JsonSerializer.Serialize(requestBody);


        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");


        var response = await _httpClient.PostAsync(
            "https://api.openai.com/v1/responses",
            content);


        response.EnsureSuccessStatusCode();


        var responseJson =
            await response.Content.ReadAsStringAsync();

            Console.WriteLine("========== OPENAI RESPONSE ==========");
            Console.WriteLine(responseJson);
            Console.WriteLine("====================================");


        using var document =
            JsonDocument.Parse(responseJson);


        var output =
            document.RootElement
            .GetProperty("output")[0]
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();


        var result = JsonSerializer.Deserialize<AIAssignment>(
        output!,
        new JsonSerializerOptions
        
        {
            PropertyNameCaseInsensitive = true
        });

Console.WriteLine("===== DESERIALIZED OBJECT =====");
Console.WriteLine($"Category: {result?.Category}");
Console.WriteLine($"Priority: {result?.Priority}");
Console.WriteLine($"Engineer: {result?.AssignedEngineer}");
Console.WriteLine($"Reason: {result?.Reason}");
Console.WriteLine("===============================");


        return result!;
    }
}