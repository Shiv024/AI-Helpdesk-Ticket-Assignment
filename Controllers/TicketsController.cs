using HelpdeskAPI.Data;
using HelpdeskAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
    {
        return await _context.Tickets.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTickets), new { id = ticket.Id }, ticket);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
{
    var ticket = await _context.Tickets.FindAsync(id);

    if (ticket == null)
    {
        return NotFound();
    }

    return ticket;
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
{
    if (id != ticket.Id)
    {
        return BadRequest();
    }

    _context.Entry(ticket).State = EntityState.Modified;

    await _context.SaveChangesAsync();

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTicket(int id)
{
    var ticket = await _context.Tickets.FindAsync(id);

    if (ticket == null)
    {
        return NotFound();
    }

    _context.Tickets.Remove(ticket);

    await _context.SaveChangesAsync();

    return NoContent();
}

[HttpGet("highpriority")]
public IActionResult GetHighPriorityTickets()
{
    var tickets = _context.Tickets
        .Where(t => t.Priority == "High")
        .ToList();

    return Ok(tickets);
}

[HttpGet("assigned")]
public IActionResult GetAssignedTickets()
{
    var tickets = _context.Tickets
        .Where(t => t.Status == "Assigned")
        .ToList();

    return Ok(tickets);
}

[HttpGet("new")]
public IActionResult GetNewTickets()
{
    var tickets = _context.Tickets
        .Where(t => t.Status == "New")
        .ToList();

    return Ok(tickets);
}

[HttpGet("engineer/{name}")]
public IActionResult GetByEngineer(string name)
{
    var tickets = _context.Tickets
        .Where(t => t.AssignedEngineer == name)
        .ToList();

    return Ok(tickets);
}

[HttpGet("category/{category}")]
public IActionResult GetByCategory(string category)
{
    var tickets = _context.Tickets
        .Where(t => t.Category == category)
        .ToList();

    return Ok(tickets);
}

}