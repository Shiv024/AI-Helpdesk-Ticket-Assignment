namespace HelpdeskAPI.Models;

public class Engineer
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Skills { get; set; } = "";

    public int CurrentTickets { get; set; }
}