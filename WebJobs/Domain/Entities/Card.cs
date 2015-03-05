using System;

namespace Domain
{
    public class Card
    {
        public string SetName { get; set; }
        public string Name { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
        public DateTime Date { get; set; }

        public string GetEntity()
        {
            return string.Format("{0},{1}", this.Name, this.SetName);
        }
    }
}
