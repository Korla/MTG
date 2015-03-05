using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Infrastructure;
using Domain;
using System;

namespace TestProgram
{
    public class Functions
    {
        static TextWriter l;
        // This function will be triggered based on the schedule you have set for this WebJob
        // This function will enqueue a message on an Azure Queue called queue
        [NoAutomaticTrigger]
        public static void ManualTrigger(TextWriter log, int value, [Queue("queue")] out string message)
        {
            message = value.ToString();

            var lines = new EventRepository<RawHtml>().Get("SuperNovaCards");
            lines.ToList().ForEach(ParseCards);
        }

        public static void ParseCards(RawHtml cards)
        {
            Console.WriteLine("Begin parse: {0}", cards.date);
            var parser = new SuperNovaParser(cards);

            var cardRepository = new EventRepository<Card>();
            Console.WriteLine("Saving cards: {0}", parser.cards.Count());
            cardRepository.Create(parser.cards, (card => card.GetEntity()));

            var setRepository = new EventRepository<Set>();
            Console.WriteLine("Saving sets: {0}", parser.cards.Count());
            setRepository.Create(parser.sets, (set => set.GetEntity()));
        }
    }
}
