using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Infrastructure;
using Domain;
using System;

namespace ParseSuperNovaCards
{
    public class Functions
    {
        public static void WaitForMessage([QueueTrigger("SuperNovaCards")] DateTime date)
        {
            var rawCards = new EventRepository<RawHtml>().Get("RawSuperNovaCards").First(x => x.date == date);
            var parser = new SuperNovaParser(rawCards);

            var cardRepository = new EventRepository<Card>();
            parser.cards.ToList().ForEach(card => cardRepository.Create(card.Name, card));

            var setRepository = new EventRepository<Set>();
            parser.sets.ToList().ForEach(set => setRepository.Create(set.Name, set));
        }
    }
}
