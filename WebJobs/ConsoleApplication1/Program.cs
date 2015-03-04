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

            //ParseCards();

            var cards = new EventRepository<Card>().Get();

            Console.ReadLine();
        }

        public static void ParseCards(RawHtml cards)
        {
            var parser = new SuperNovaParser(cards);

            var cardRepository = new EventRepository<Card>();
            parser.cards.ToList().ForEach(card => cardRepository.Create(card.Name, card));

            var setRepository = new EventRepository<Set>();
            parser.sets.ToList().ForEach(set => setRepository.Create(set.Name, set));
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
