using System;

namespace PokerOnline
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.SetWindowSize(65, 40);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            // renuntam la scroll bar 
            //setand bufferul la dimensiunea actuala a ferestrei
            Console.BufferHeight = 40;
            
            Console.Title = "Joc de Poker";
            DealCards dc = new DealCards();
            bool quit = false;

            while( !quit)
            {
                dc.Deal();

                char selection = ' ';
                while(!selection.Equals('Y') && !selection.Equals('N'))
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
