using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Infrastructure;
using System.Globalization;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetCardsAndBoosters();

            //ParseCards();

            var cards = new EventRepository<RawHtml>().Get("SuperNovaCards").First();
            ParseCards(cards);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void ParseCards(RawHtml cards)
        {
            var parser = new SuperNovaParser(cards);

            var cardRepository = new EventRepository<Card>();
            cardRepository.Create(parser.cards, (card => card.GetEntity()));

            var setRepository = new EventRepository<Set>();
            setRepository.Create(parser.sets, (set => set.GetEntity()));
        }
        public static void GetCardsAndBoosters()
        {
            var cardsUrl = "http://supernovabots.com/prices_0.txt";
            var cardsText = new HttpService().Get(cardsUrl);
            var rawCards = new RawHtml { Text = cardsText, Url = cardsUrl };
            
            new EventRepository<RawHtml>().Create("SuperNovaCards", rawCards);

            var boostersUrl = "http://supernovabots.com/prices_6.txt";
            var boostersText = new HttpService().Get(boostersUrl);
            var rawBoosters = new RawHtml { Text = cardsText, Url = boostersUrl };
            
            new EventRepository<RawHtml>().Create("SuperNovaBoosters", rawBoosters);
        }
    }
}
