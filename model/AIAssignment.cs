namespace HelpdeskAPI.Models;

public class AIAssignment
{
    public string Category { get; set; } = "";
    public string Priority { get; set; } = "";
    public string AssignedEngineer { get; set; } = "";
    public string Reason { get; set; } = "";
}