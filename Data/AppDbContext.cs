using Microsoft.EntityFrameworkCore;
using HelpdeskAPI.Models;

namespace HelpdeskAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Engineer> Engineers { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }
}