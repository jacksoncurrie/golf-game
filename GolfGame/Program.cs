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

            Game game = new Game();

            for (int i = 0; i < 9; i++)
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
                        Console.Write
                        (
                            "===================\n" +
                            "== Invalid Input ==\n" +
                            "==   Try Again   ==\n" +
                            "===================\n" +
                            "\n" +
                            "Press any key to continue..."
                        );
                        Console.ReadKey();
                        Console.Clear();
                        game.DisplayHole();
                        continue;
                    }

                    // Power in range
                    if (power < 1 || power > 100)
                    {
                        Console.Clear();
                        Console.Write
                        (
                            "===================\n" +
                            "== Invalid Input ==\n" +
                            "==   Try Again   ==\n" +
                            "===================\n" +
                            "\n" +
                            "Press any key to continue..."
                        );
                        Console.ReadKey();
                        Console.Clear();
                        game.DisplayHole();
                        continue;
                    }

                    game.Power = power;

                    // Get club input
                    Console.Write("Choose Club (3, 5, 7, or 9 iron)...");

                    switch (Console.ReadKey(true).Key)
                    {
                        // Number 3
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            game.Club = 0.003;
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
                            Console.Write
                            (
                                "===================\n" +
                                "== Invalid Input ==\n" +
                                "==   Try Again   ==\n" +
                                "===================\n" +
                                "\n" +
                                "Press any key to continue..."
                            );
                            Console.ReadKey();
                            Console.Clear();
                            game.DisplayHole();
                            continue;
                    }

                    // Draw path
                    game.Path();
                }
                while (!game.HasWon);

                Console.Clear();
                game.CurrentHoleNum++;

                game.DisplayScoreCard();
            }

            Console.WriteLine
            (
                "\n" +
                "=========================\n" +
                "===    End of Game    ===\n" +
                "=========================\n"
            );

            game.DisplayScoreCard();
        }
    }
}
