using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Domain;
using Infrastructure;

namespace ParseSuperNovaCards
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("RawSuperNovaCards")] string rawHtmlId, TextWriter log)
        {
            Console.WriteLine("Fetching from database, date: " + rawHtmlId);

            var superNovaCardsHtml = new EventRepository<RawHtml>().Get(Int32.Parse(rawHtmlId));

            Console.WriteLine("Begin parsing cards, date: " + rawHtmlId);

            var parser = new SuperNovaParser(superNovaCardsHtml);

            var cardRepository = new EventRepository<Card>();
            parser.cards.ToList().ForEach(card => cardRepository.Create(card.Name, card));

            var setRepository = new EventRepository<Set>();
            parser.sets.ToList().ForEach(set => setRepository.Create(set.Name, set));

            Console.WriteLine("Done parsing cards, date: " + rawHtmlId);
        }
    }
}
