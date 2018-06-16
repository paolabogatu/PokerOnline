using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerOnline
{
    class DeckOfCards : Card
    {
        const int NUM_OF_CARDS = 52; //numerele tuturor cartilor
        private Card[] deck; //array la toate cartile

        public DeckOfCards()
        {
            deck = new Card[NUM_OF_CARDS];

        }

        public Card[] getDeck { get { return deck; } } // preia cartile curente

        //create deck of 52 card: 13 valori fiecare, cu 4 perechi
        public void setUpDeck()
        {
            int i = 0;
            foreach(SUIT s in Enum.GetValues(typeof(SUIT)))
            {
                foreach(VALUE v in Enum.GetValues(typeof(VALUE)))
                {
                    deck[i] = new Card { MySuit = s, MyValue = v };
                    i++;
                }
            }
            ShuffleCards();
        }
            //amesteca toate cartile
        public void ShuffleCards()
        {
            Random rand = new Random();
            Card temp;

            //run the shuffle 1000 times
            for(int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
            {
                for (int i = 0; i< NUM_OF_CARDS; i++)
                {
                    //schimba cartile
                    int secondCardIndex = rand.Next(13);
                    temp = deck[i];
                    deck[i] = deck[secondCardIndex];
                    deck[secondCardIndex] = temp;
                }
            }
        }
    }
}
