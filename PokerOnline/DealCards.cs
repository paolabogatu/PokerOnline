using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PokerOnline
{
    class DealCards : DeckOfCards
    {
        private Card[] playerHand;
        private Card[] opponentHand;
        private Card[] sortedPlayerHand;
        private Card[] sortedOpponentHand;

        public DealCards()
        {
            playerHand = new Card[5];
            sortedPlayerHand = new Card[5];
            opponentHand = new Card[5];
            sortedOpponentHand = new Card[5];


        }
        public void Deal(Socket clientSocket)
        {
            setUpDeck(); //creaza cartile si le amesteca
            getHand();
            sortCards();
            displayCardsForPlayers(clientSocket);
            evaluateHands();
        }

        public void getHand()
        {
            //5 carti pentru jucator
            for (int i = 0; i < 5; i++)
                playerHand[i] = getDeck[i];

            //5 carti pentru pc
            for (int i = 5; i < 10; i++)
                opponentHand[i - 5] = getDeck[i];
        }

        public void sortCards()
        {
            var queryPlayer = from hand in playerHand
                              orderby hand.MyValue
                              select hand;
            var queryOpponent = from hand in opponentHand
                                orderby hand.MyValue
                                select hand;

            var index = 0;
            foreach(var element in queryPlayer.ToList())
            {
                sortedPlayerHand[index] = element;
                index++;
            }

            index = 0;
            foreach(var element in queryOpponent.ToList())
            {
                sortedOpponentHand[index] = element;
                index++;
            }
        }

        public void displayCardsForPlayers(Socket clientSocket)
        {
            string clientCards = sortedOpponentHand[0].ToString() + "|"
                + sortedOpponentHand[1].ToString() + "|"
                + sortedOpponentHand[2].ToString() + "|"
                + sortedOpponentHand[3].ToString() + "|"
                + sortedOpponentHand[4].ToString();

            string serverCards = sortedPlayerHand[0].ToString() + "|"
                + sortedPlayerHand[1].ToString() + "|"
                + sortedPlayerHand[2].ToString() + "|"
                + sortedPlayerHand[3].ToString() + "|"
                + sortedPlayerHand[4].ToString();

            string msgToSend = "CARDS|" + clientCards + "|" + serverCards;

            clientSocket.Send(Encoding.ASCII.GetBytes(msgToSend));

            // Now display the cards for the server.
            displayCards(sortedPlayerHand, sortedOpponentHand);
        }

        public static void displayCards(Card[] sortedPlayerHand, Card[] sortedOpponentHand)
        {
            Console.Clear(); 
            int x = 0; // pozitia cursorului pe care il mutam la stanga si dreapta
            int y = 1; // pozitia y a cursorului , pe care il mutam sus si jos

            //display player hand
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Mana ta");
            for (int i = 0; i < 5; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayerHand[i], x, y);
                x++; // muta la dreapta
            }

            y = 15; // muta randul adversarului sub  cartile jucatorului
            x = 0; // reseteaza pozitia lui x
            Console.SetCursorPosition(x, 14);
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("Mana oponentului");
            for (int i = 5; i< 10; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedOpponentHand[i-5], x, y);
                x++; // muta la dreapta
            }
        }

        public void evaluateHands()
        {
            HandEvaluator playerHandEvaluatoar = new HandEvaluator(sortedPlayerHand);
            HandEvaluator opponentHandEvaluator = new HandEvaluator(sortedOpponentHand);
            
            Hand playerHand = playerHandEvaluatoar.EvaluateHand();
            Hand opponentHand = opponentHandEvaluator.EvaluateHand();

            // aratam ambele maini
            Console.WriteLine("\n\n\n\n\nMana ta: " + playerHand);
            Console.WriteLine("\nMana oponent: " + opponentHand);

            //evaluam mainile
            if(playerHand > opponentHand)
            {
                Console.WriteLine("Tu ai castigat!");
            }
            else if(playerHand < opponentHand)
            {
                Console.WriteLine("Adversarul a CASTIGAT!");
            }
            else //daca mainile sunt la fel, se evalueaza valorile
            {
                //prima evaluare, pentru cel care are cea mai mare valoare
                if (playerHandEvaluatoar.HandValues.Total > opponentHandEvaluator.HandValues.Total)
                    Console.WriteLine("Tu ai CASTIGAT!");
                else if (playerHandEvaluatoar.HandValues.Total < opponentHandEvaluator.HandValues.Total)
                    Console.WriteLine("Adversarul a CASTIGAT!");
                //daca au aceleasi valori
                //jucatorul cu urmatoarea carte castiga
                else if (playerHandEvaluatoar.HandValues.HighCard > opponentHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("Tu ai CASTIGAT!");
                else if (playerHandEvaluatoar.HandValues.HighCard < opponentHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("Adversarul a CASTIGAT!");
                else
                    Console.WriteLine("Nimeni nu a castigat!");
            }
        }
    }
}
