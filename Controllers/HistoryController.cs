using HelpdeskAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpdeskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public HistoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{ticketId}")]
    public IActionResult GetHistory(int ticketId)
    {
        var history = _context.TicketHistories
            .Where(h => h.TicketId == ticketId)
            .OrderBy(h => h.Date)
            .ToList();

        return Ok(history);
    }
}