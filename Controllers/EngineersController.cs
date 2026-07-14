using HelpdeskAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EngineersController : ControllerBase
{
    private readonly AppDbContext _context;

    public EngineersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEngineers()
    {
        return Ok(await _context.Engineers.ToListAsync());
    }
}