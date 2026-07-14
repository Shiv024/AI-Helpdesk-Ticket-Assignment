using HelpdeskAPI.Models;

namespace HelpdeskAPI.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Engineers.Any())
            return;

        context.Engineers.AddRange(

            new Engineer
            {
                Name = "John",
                Skills = "ERP,Authentication",
                CurrentTickets = 0
            },

            new Engineer
            {
                Name = "David",
                Skills = "SQL,Database",
                CurrentTickets = 0
            },

            new Engineer
            {
                Name = "Peter",
                Skills = "VPN,Network",
                CurrentTickets = 0
            }

        );

        context.SaveChanges();
    }
}