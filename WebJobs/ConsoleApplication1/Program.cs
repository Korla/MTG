using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Infrastructure;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetCardsAndBoosters();

            ParseCards();

            var i = 1;
            foreach(var c in new EventRepository<RawSuperNovaCards>().Get())
            {
                Console.WriteLine(i++);
            }

            Console.ReadLine();
        }

        public static void ParseCards()
        {
            var lines = new EventRepository<RawSuperNovaCards>().Get().First().Text.Split(new string[] { "\n" }, StringSplitOptions.None);

            var parser = new SuperNovaParser(lines);

            var cardRepository = new EventRepository<Card>();
            parser.cards.ToList().ForEach(cardRepository.Create);

            var setRepository = new EventRepository<Set>();
            parser.sets.ToList().ForEach(setRepository.Create);

            new EventRepository<Card>().Get().ToList().ForEach(x => Console.WriteLine(x.Name));
        }
        public static void GetCardsAndBoosters()
        {
            var cardsUrl = "http://supernovabots.com/prices_0.txt";
            var cardsText = new HttpService().Get(cardsUrl);
            var rawCards = new RawSuperNovaCards { Text = cardsText, Url = cardsUrl };
            
            new EventRepository<RawSuperNovaCards>().Create(rawCards);

            var boostersUrl = "http://supernovabots.com/prices_6.txt";
            var boostersText = new HttpService().Get(boostersUrl);
            var rawBoosters = new RawSuperNovaBoosters { Text = cardsText, Url = boostersUrl };
            
            new EventRepository<RawSuperNovaBoosters>().Create(rawBoosters);
        }
    }
}
