namespace Domain
{
    using System;

    public class Event
    {
        public DateTime date = DateTime.Now;

        public int Id { get; set; }

        public string Json { get; set; }

        public string Type { get; set; }
    }
}
