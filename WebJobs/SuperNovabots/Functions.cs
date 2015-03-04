using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Domain;
using Infrastructure;
using System;

namespace SuperNovabots
{
    public class Functions
    {
        // This function will be triggered based on the schedule you have set for this WebJob
        // This function will enqueue a message on an Azure Queue called queue
        [NoAutomaticTrigger]
        public static void ManualTrigger(TextWriter log, int value)
        {
            var cardsUrl = "http://supernovabots.com/prices_0.txt";
            var cardsText = new HttpService().Get(cardsUrl);
            var rawCards = new RawHtml { Text = cardsText, Url = cardsUrl };

            log.WriteLine("Storing RawSuperNovaCards");
            new EventRepository<RawHtml>().Create("SuperNovaCards", rawCards);

            var boostersUrl = "http://supernovabots.com/prices_6.txt";
            var boostersText = new HttpService().Get(boostersUrl);
            var rawBoosters = new RawHtml { Text = cardsText, Url = boostersUrl };

            log.WriteLine("Storing RawSuperNovaBoosters");
            new EventRepository<RawHtml>().Create("SuperNovaBoosters", rawBoosters);
        }
    }
}
