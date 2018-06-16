using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerOnline
{
    public enum Hand
    {
        Nothing,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind
    }


    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }
    class HandEvaluator : Card
    {
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Card[] cards;
        private HandValue handValue;

        public HandEvaluator(Card[] sortedHand)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            cards = new Card[5];
            cards = sortedHand;
            handValue = new HandValue();
        }

        public HandValue HandValues
        {
            get { return handValue;  }
            set { handValue = value; }
        }

        public Card[] Cards
        {
            get { return cards;  }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];

            }
        }

        public Hand EvaluateHand()
        {
            //preluam numerele fiecarei perechi din mana
            getNumberOfSuit();
            if (FourOfKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeOfKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;

            //daca mana este "nimic" atunci jucatorul cu cea mai mare
            //carte castiga
            handValue.HighCard = (int)cards[4].MyValue;
            return Hand.Nothing;
        }
        private void getNumberOfSuit()
        {
            foreach(var element in Cards)
            {
                if (element.MySuit == Card.SUIT.HEARTS)
                    heartsSum++;
                else if (element.MySuit == Card.SUIT.DIAMONDS)
                    diamondSum++;
                else if (element.MySuit == Card.SUIT.CLUBS)
                    clubSum++;
                else if (element.MySuit == Card.SUIT.SPADES)
                    spadesSum++;
            }
        }
        private bool FourOfKind()
        {
            // adaugam valori pentru cele 4 carti si ultima carte
            //e cea mai mare

            if(cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[0].MyValue == cards[3].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue && cards[1].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[0].MyValue;
                return true;

            }
            return false;

        }
        private bool FullHouse()
        {
            //primele trei carti si ultimele 2 au aceiasi valoare
            //primele 2 si ultimele 3 au aceiasi valoare
            if ((cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue) ||
                 (cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue))
            {
                handValue.Total = (int)(cards[0].MyValue) + (int)(cards[1].MyValue) + (int)(cards[2].MyValue) +
                    (int)(cards[3].MyValue) + (int)(cards[4].MyValue);
                return true;
                }

            return false;

        }
        private bool Flush()
        {
            if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum ==5)
            {

                handValue.Total = (int)cards[4].MyValue;
                return true;
            }
            return false;
        }
        private bool Straight()
        {
            //daca 5 valori consecutive 

            if (cards[0].MyValue + 1 == cards[1].MyValue &&
                cards[1].MyValue + 1 == cards[2].MyValue &&
                cards[2].MyValue + 1 == cards[3].MyValue &&
                cards[3].MyValue + 1 == cards[4].MyValue)
            {
                // jucatorul cu cea mai mare valoare a ultimei carti castiga
                handValue.Total = (int)cards[4].MyValue;
                return true;
            }
            return false;
    }
        private bool ThreeOfKind()
        {
            //daca 1, 2, 3 carti sunt la fel SAU
            // 2, 3, 4 sunt la fel SAU
            //3, 4, 5 sunt la fel 
            //Cartile de pe pozitia 3 sunt ThreeOfKind
            if((cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue) ||
                (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue))
            {
                handValue.Total = (int)cards[2].MyValue * 3;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }

            else if (cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[2].MyValue * 3;
                handValue.HighCard = (int)cards[1].MyValue;
                return true;
            }
            return false;
       
        }
        private bool TwoPairs()
        {
            //daca 1, 2 si 3, 4
            //sau daca 1,2 si 4,5 sunt la fel
            //sau 2, 3 si 4,5 sunt la fel
            if(cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue * 2);
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[0].MyValue == cards[1].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue * 2);
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue * 2);
                handValue.HighCard = (int)cards[0].MyValue;
                return true;
            }
            return false;
        }

        private bool OnePair()
        {
            //daca 1, 2  -> cartea #5 are valoarea cea mai mare
            //sau 2, 3
            //sau 3, 4
            //sau 4, 5 -> cartea #3 are cea mai mare valoare

            if (cards[0].MyValue == cards[1].MyValue)
            {
                handValue.Total = (int)cards[0].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[2].MyValue == cards[3].MyValue)
            {
                handValue.Total = (int)cards[2].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[3].MyValue * 2;
                handValue.HighCard = (int)cards[2].MyValue;
                return true;
            }
            return false;

        }
    }

    
}
