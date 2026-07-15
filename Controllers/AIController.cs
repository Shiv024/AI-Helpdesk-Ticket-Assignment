using HelpdeskAPI.Data;
using HelpdeskAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly OpenAIService _openAI;

    public AIController(AppDbContext context, OpenAIService openAI)
    {
        _context = context;
        _openAI = openAI;
    }

    [HttpPost("assign/{id}")]
    public async Task<IActionResult> AssignTicket(int id)
    {
        // Find the ticket
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        // Ask OpenAI to analyze the ticket
        var ai = await _openAI.AnalyzeTicket(ticket.Title, ticket.Description);

        // Update the ticket with AI results
        ticket.Category = ai.Category;
        ticket.Priority = ai.Priority;
        ticket.AssignedEngineer = ai.AssignedEngineer;
        ticket.AIReason = ai.Reason;
        ticket.Status = "Assigned";

        // Find the assigned engineer
        var engineer = await _context.Engineers
            .FirstOrDefaultAsync(e => e.Name == ai.AssignedEngineer);

        // Increase their workload
        if (engineer != null)
        {
            engineer.CurrentTickets++;
        }

        // Save all changes
        await _context.SaveChangesAsync();

        // Return the updated ticket
        return Ok(ticket);
    }
}