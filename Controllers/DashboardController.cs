using HelpdeskAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpdeskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetDashboard()
    {
        var dashboard = new
        {
            totalTickets = _context.Tickets.Count(),

            newTickets = _context.Tickets.Count(t =>
                t.Status == "New"),

            assignedTickets = _context.Tickets.Count(t =>
                t.Status == "Assigned"),

            closedTickets = _context.Tickets.Count(t =>
                t.Status == "Closed"),

            highPriorityTickets = _context.Tickets.Count(t =>
                t.Priority == "High"),

            totalEngineers = _context.Engineers.Count()
        };

        return Ok(dashboard);
    }
}