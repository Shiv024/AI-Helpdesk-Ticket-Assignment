using HelpdeskAPI.Data;
using HelpdeskAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpdeskAPI.Models; 

namespace HelpdeskAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly OpenAIService _openAI;


    public AIController(
        AppDbContext context,
        OpenAIService openAI)
    {
        _context = context;
        _openAI = openAI;
    }



    [HttpPost("assign/{id}")]
    public async Task<IActionResult> AssignTicket(int id)
    {

        var ticket =
            await _context.Tickets.FindAsync(id);


        if(ticket == null)
        {
            return NotFound();
        }


        var result =
            await _openAI.AnalyzeTicket(
                ticket.Title,
                ticket.Description);


        ticket.Category = result.Category;
        ticket.Priority = result.Priority;
        ticket.AssignedEngineer = result.AssignedEngineer;
        ticket.AIReason = result.Reason;
        ticket.Status = "Assigned";

        await _context.SaveChangesAsync();

        _context.TicketHistories.Add(new TicketHistory
        {
            TicketId = ticket.Id,
            Action = $"Assigned to {ticket.AssignedEngineer}",
            PerformedBy = "AI",
            Date = DateTime.Now
        });

await _context.SaveChangesAsync();


        return Ok(ticket);
    }

}