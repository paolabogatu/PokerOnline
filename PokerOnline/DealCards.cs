using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerOnline
{
    class DealCards : DeckOfCards
    {
        private Card[] playerHand;
        private Card[] computerHand;
        private Card[] sortedPlayerHand;
        private Card[] sortedComputerHand;

        public DealCards()
        {
            playerHand = new Card[5];
            sortedPlayerHand = new Card[5];
            computerHand = new Card[5];
            sortedComputerHand = new Card[5];


        }
        public void Deal()
        {
            setUpDeck(); //creaza cartile si le amesteca
            getHand();
            sortCards();
            displayCards();
            evaluateHands();
        }

        public void getHand()
        {
            //5 carti pentru jucator
            for (int i = 0; i < 5; i++)
                playerHand[i] = getDeck[i];

            //5 carti pentru pc
            for (int i = 5; i < 10; i++)
                computerHand[i - 5] = getDeck[i];


        }

        public void sortCards()
        {
            var queryPlayer = from hand in playerHand
                              orderby hand.MyValue
                              select hand;
            var queryComputer = from hand in computerHand
                                orderby hand.MyValue
                                select hand;

            var index = 0;
            foreach(var element in queryPlayer.ToList())
            {
                sortedPlayerHand[index] = element;
                index++;
            }

            index = 0;
            foreach(var element in queryComputer.ToList())
            {
                sortedComputerHand[index] = element;
                index++;
            }
        }
        public void displayCards()
        {
            Console.Clear(); 
            int x = 0; // pozitia cursorului pe care il mutam la stanga si dreapta
            int y = 1; // pozitia y a cursorului , pe care il mutam sus si jos

            //display player hand
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Mana jucatorului");
            for (int i = 0; i < 5; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayerHand[i], x, y);
                x++; // muta la dreapta 

            }
            y = 15; // muta randul pc-ului sub  cartile jucatorului
            x = 0; // reseteaza pozitia lui x
            Console.SetCursorPosition(x, 14);
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("Mana CPU");
            for (int i = 5; i< 10; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedComputerHand[i-5], x, y);
                x++; // muta la dreapta
            }


        }

        public void evaluateHands()
        {
            //jucatorii si CPU sunt creati
            HandEvaluator playerHandEvaluatoar = new HandEvaluator(sortedPlayerHand);
            HandEvaluator computerHandEvaluator = new HandEvaluator(sortedComputerHand);


            Hand playerHand = playerHandEvaluatoar.EvaluateHand();
            Hand computerHand = computerHandEvaluator.EvaluateHand();

            // aratam ambele maini
            Console.WriteLine("\n\n\n\n\nMana jucatorului: " + playerHand);
            Console.WriteLine("\nMana CPU: " + computerHand);

            //evaluam mainile
            if(playerHand > computerHand)
            {
                Console.WriteLine("Jucatorul a castigat!");
            }
            else if(playerHand < computerHand)
            {
                Console.WriteLine("CPU a CASTIGAT!");
            }
            else //daca mainile sunt la fel, se evalueaza valorile
            {
                //prima evaluare, pentru cel care are cea mai mare valoare
                if (playerHandEvaluatoar.HandValues.Total > computerHandEvaluator.HandValues.Total)
                    Console.WriteLine("Jucatorul a CASTIGAT!");
                else if (playerHandEvaluatoar.HandValues.Total < computerHandEvaluator.HandValues.Total)
                    Console.WriteLine("CPU a CASTIGAT!");
                //daca au aceleasi valori
                //jucatorul cu urmatoarea carte castiga
                else if (playerHandEvaluatoar.HandValues.HighCard > computerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("Jucatorul a CASTIGAT!");
                else if (playerHandEvaluatoar.HandValues.HighCard < computerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("CPU a CASTIGAT!");
                else
                    Console.WriteLine("Nimeni nu a castigat!");

            }

        }
    }
}
