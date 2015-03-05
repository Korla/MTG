using Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Infrastructure
{
    public class SuperNovaParser
    {
        public SuperNovaParser(RawHtml html)
        {
            var date = html.date;
            var lines = html.Text.Split(new string[] { "\n" }, StringSplitOptions.None).Where(x => x != string.Empty);
            var masterLine = lines.First(x => x.StartsWith("Name"));
            var indexOfBuy = masterLine.IndexOf("Buy");
            var indexOfSell = masterLine.IndexOf("Sell");
            var indexOfStock = masterLine.IndexOf("Bot");

            var setLines = lines.Select((line, index) => new { line, index }).Where(x => x.line.StartsWith("==="));

            var cardsBySet = lines.Select((line, index) => new { line, index }).Where(x => x.line.StartsWith("==="))
                .Select(setLine =>
                    new
                    {
                        name = setLine.line.Split(new string[] { "===" }, StringSplitOptions.None)[1].Trim(),
                        cardRows = lines.Skip(setLine.index + 1).TakeWhile(x => !x.StartsWith("===")).ToList()
                    });

            this.cards = cardsBySet
                .SelectMany(set =>
                    set.cardRows
                        .Select(cardRow =>
                            new Card
                            {
                                SetName = set.name,
                                Name = cardRow.Split('[')[0].Trim(),
                                Buy = this.GetCost(cardRow, indexOfBuy),
                                Sell = this.GetCost(cardRow, indexOfSell),
                                Date = date
                            }
                        ));

            this.sets = this.cards
                .GroupBy(card => card.SetName)
                .Select(cardGroup =>
                    new Set
                    {
                        Name = cardGroup.Key,
                        Cards = cardGroup.Select(card => card.Name).ToList(),
                        TotalBuy = cardGroup.Sum(card => card.Buy),
                        TotalSell = cardGroup.Sum(card => card.Sell),
                        Date = date
                    });
        }

        public IEnumerable<Card> cards { get; set; }
        public IEnumerable<Set> sets { get; set; }

        private double GetCost(string cardRow, int position)
        {
            var array = cardRow.Skip(position).TakeWhile(x => x.ToString() != " ").ToArray();
            return array.Length > 0 ? Double.Parse(new string(array), CultureInfo.InvariantCulture) : 0;
        }
    }
}
