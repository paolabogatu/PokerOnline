using System;
using System.Threading;
using System.Windows.Forms;

namespace PokerOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            PrepareConsole();

            // Ask clients if they want to host a new game or join an existing one.
            ServerOrClient();

            Thread thread = new Thread(() => StartChatWindow());
            thread.Start();

            GameLoop();
        }

        private static void StartChatWindow()
        {
            Application.EnableVisualStyles();
            Application.Run(new ChatWindow());
        }

        private static void PrepareConsole ()
        {
            Console.SetWindowSize(65, 40);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            // renuntam la scroll bar 
            //setand bufferul la dimensiunea actuala a ferestrei
            Console.BufferHeight = 40;

            Console.Title = "Joc de Poker";
        }

        private static void ServerOrClient ()
        {
            Console.WriteLine("Do you wanna host a new game? Y-N");

            bool wantsNewGame = false;
            char selection = ' ';
            while (!selection.Equals('Y') && !selection.Equals('N'))
            {
                Console.WriteLine("Play again? Y-N");
                selection = Convert.ToChar(Console.ReadLine().ToUpper());

                if (selection.Equals('Y'))
                    wantsNewGame = true;
                else if (selection.Equals('N'))
                    wantsNewGame = false;
                else
                    Console.WriteLine("Invalid Selection. Try again");
            }

            if (wantsNewGame)
            {
                // Start server.


            }
            else
            {
                // Connect to a server.


            }
        }

        private static void GameLoop()
        {

            DealCards dc = new DealCards();
            bool quit = false;

            // Bucla de joc.
            while (!quit)
            {
                dc.Deal();

                char selection = ' ';
                while (!selection.Equals('Y') && !selection.Equals('N'))
                {
                    Console.WriteLine("Play again? Y-N");
                    selection = Convert.ToChar(Console.ReadLine().ToUpper());

                    if (selection.Equals('Y'))
                        quit = false;
                    else if (selection.Equals('N'))
                        quit = true;
                    else
                        Console.WriteLine("Invalid Selection. Try again");

                }

            }
        }
    }

}
