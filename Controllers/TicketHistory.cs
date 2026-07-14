namespace HelpdeskAPI.Models;

public class TicketHistory
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public string Action { get; set; } = "";

    public string PerformedBy { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.Now;
}