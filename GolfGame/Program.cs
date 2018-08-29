using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame
{
    class Program
    {
        static void Main()
        {
            // Set height of console
            Console.BufferHeight = 45;
            Console.WindowHeight = 46;
            Console.BufferWidth = 121;
            Console.WindowWidth = 122;

            ConsoleKeyInfo key;
            bool playAgain = true;
            Game game = new Game();

            do
            {
                game.StartHole();
                // Loop until won
                do
                {
                    
                    // Get power input
                    Console.Write("Enter power (1-100): ");
                    if (!Int32.TryParse(Console.ReadLine(), out int power))
                    {
                        Console.Clear();
                        game.DisplayHole();
                        continue;
                    }
                        
                    // Power in range
                    if (power < 1 || power > 100)
                    {
                        Console.Clear();
                        continue;
                    }
                        

                    game.Power = power;

                    // Get club input
                    Console.Write("Choose Club (3, 5, 7, or 9 iron)");

                    // Set club value
                    double club;
                    switch (Console.ReadKey(true).Key)
                    {
                        // Number 3
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            game.Club = 0.002;
                            game.Power += 30;
                            break;

                        // Number 5
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            game.Club = 0.006;
                            game.Power += 20;
                            break;

                        // Number 7
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            game.Club = 0.01;
                            game.Power += 10;
                            break;

                        // Number 9
                        case ConsoleKey.NumPad9:
                        case ConsoleKey.D9:
                            game.Club = 0.014;
                            break;

                        // Other value
                        default:
                            Console.Clear();
                            game.DisplayHole();
                            continue;
                    }

                    // Draw path
                    game.Path();

                    // Wait for input
                    Console.ReadKey();
                }
                while (!game.HasWon);

                

                Console.Clear();

                do
                {
                    // Get input
                    Console.Write("\nDo you want to play again? [Y]/[N]");
                    key = Console.ReadKey(true);

                    // Check key
                    switch (key.Key)
                    {
                        // Y
                        case ConsoleKey.Y:
                            playAgain = true;
                            break;

                        // N
                        case ConsoleKey.N:
                            playAgain = false;
                            break;

                        // Wrong input
                        default:
                            Console.Clear();
                            Console.WriteLine("\nThat is not an option.");
                            break;
                    }
                }
                while (key.Key != ConsoleKey.N && key.Key != ConsoleKey.Y);

            }
            while (playAgain);
        }
    }
}
