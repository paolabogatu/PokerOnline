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

                Console.WriteLine("Player name:");
                string name = Console.ReadLine();

                Server server = new Server(name);
            }
            else
            {
                // Connect to a server.

                Console.WriteLine("Player name:");
                string name = Console.ReadLine();

                Console.WriteLine("Type the IP address of the server.");
                string address = Console.ReadLine();

                Client client = new Client(address, name);
            }
        }
    }

}
