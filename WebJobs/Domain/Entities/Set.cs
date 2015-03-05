using System.Collections.Generic;

using System;

namespace Domain
{
    public class Set
    {
        public string Name { get; set; }
        public List<string> Cards { get; set; }
        public double TotalBuy { get; set; }
        public double TotalSell { get; set; }
        public DateTime Date { get; set; }

        public string GetEntity()
        {
            return this.Name;
        }
    }
}
