using System.Collections.Generic;

using System;

namespace Domain
{
    public class Set
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public double TotalBuy { get; set; }
        public double TotalSell { get; set; }
        public DateTime Date { get; set; }
    }
}
