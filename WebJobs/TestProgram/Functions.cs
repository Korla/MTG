using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Infrastructure;
using Domain;

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
            l = log;
            log.WriteLine("Function is invoked with value={0}", value);
            message = value.ToString();
            log.WriteLine("Following message will be written on the Queue={0}", message);

            var lines = new EventRepository<RawHtml>().Get("SuperNovaCards");
            lines.ToList().ForEach(ParseCards);
        }

        public static void ParseCards(RawHtml cards)
        {
            l.WriteLine("Begin parse: {0}", cards.date);
            var parser = new SuperNovaParser(cards);

            var cardRepository = new EventRepository<Card>();
            l.WriteLine("Saving cards: {0}", parser.cards.Count());
            parser.cards.ToList().ForEach(card => cardRepository.Create(card.Name, card));

            var setRepository = new EventRepository<Set>();
            l.WriteLine("Saving sets: {0}", parser.sets.Count());
            parser.sets.ToList().ForEach(set => setRepository.Create(set.Name, set));
        }
    }
}
