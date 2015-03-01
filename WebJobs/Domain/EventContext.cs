namespace Domain
{
    using System.Data.Entity;

    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }
}
