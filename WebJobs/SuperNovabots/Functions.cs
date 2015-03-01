using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Domain;
using Infrastructure;

namespace SuperNovabots
{
    public class Functions
    {
        // This function will be triggered based on the schedule you have set for this WebJob
        // This function will enqueue a message on an Azure Queue called queue
        [NoAutomaticTrigger]
        public static void ManualTrigger(TextWriter log, int value, [Queue("queue")] out string message)
        {
            log.WriteLine("Function is invoked with value={0}", value);
            message = value.ToString();
            log.WriteLine("Following message will be written on the Queue={0}", message);

            var cardsUrl = "http://supernovabots.com/prices_0.txt";
            var cardsText = new HttpService().Get(cardsUrl);
            var rawCards = new RawSuperNovaCards { Text = cardsText, Url = cardsUrl };

            log.WriteLine("Storing RawSuperNovaCards");
            new EventRepository<RawSuperNovaCards>().Create(rawCards);

            var boostersUrl = "http://supernovabots.com/prices_6.txt";
            var boostersText = new HttpService().Get(boostersUrl);
            var rawBoosters = new RawSuperNovaBoosters { Text = cardsText, Url = boostersUrl };

            log.WriteLine("Storing RawSuperNovaBoosters");
            new EventRepository<RawSuperNovaBoosters>().Create(rawBoosters);
        }
    }
}
