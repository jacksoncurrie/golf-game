using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame
{
    class Program
    {
        // Global variables
        static char[,] grid;
        static bool hasWon;

        static void Main(string[] args)
        {
            // Set height of console
            Console.BufferHeight = 45;
            Console.WindowHeight = 46;
            Console.BufferWidth = 121;
            Console.WindowWidth = 122;

            ConsoleKeyInfo key;
            bool playAgain = true;

            do
            {
                // New random number
                Random rnd = new Random();
                int hole = rnd.Next(50, 120);
                int shots = 0;

                // New Game
                hasWon = false;

                // Loop until won
                do
                {
                    // Reset Hole
                    grid = new char[40, 121];

                    // Set flag
                    grid[37, hole - 1] = '<';
                    grid[37, hole] = '|';
                    grid[38, hole] = '|';
                    grid[39, hole] = '|';

                    // Output hole
                    DisplayHole();

                    // Get power input
                    Console.Write("Enter power (1-100): ");
                    if (!Int32.TryParse(Console.ReadLine(), out int power))
                        continue;

                    // Power in range
                    if (power < 1 || power > 100)
                        continue;

                    // Get club input
                    Console.Write("Choose Club (3, 5, 7, or 9 iron)");

                    // Set club value
                    double club;
                    switch (Console.ReadKey(true).Key)
                    {
                        // Number 3
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            club = 0.002;
                            power += 30;
                            break;

                        // Number 5
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            club = 0.006;
                            power += 20;
                            break;

                        // Number 7
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            club = 0.01;
                            power += 10;
                            break;

                        // Number 9
                        case ConsoleKey.NumPad9:
                        case ConsoleKey.D9:
                            club = 0.014;
                            break;

                        // Other value
                        default:
                            continue;
                    }

                    // Add shot
                    shots++;

                    // Draw path
                    Path(club, power);

                    // Redraw hole
                    DisplayHole();

                    // Check for win
                    Console.Write(CheckLanding(hole));
                    if (hasWon)
                        Console.Write("It took {0} shots.", shots);

                    // Wait for input
                    Console.ReadKey();
                }
                while (!hasWon);

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

        static void DisplayHole()
        {
            Console.Clear();

            // Loop through Array
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 121; j++)
                    // Display values
                    Console.Write(grid[i, j]);
                Console.WriteLine();
            }

            // Write Ground
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string CheckLanding(int hole)
        {
            // Loop through bottom row
            for (int i = 120; i > 4; i--)

                // Check for location of ball
                if (grid[39, i] == '*')

                    // Check where ball lands
                    if (i == hole)
                    {
                        hasWon = true;
                        return "You won!\n\n";
                    }
                    else if (i > hole)
                        return "Too far\n\nNext Shot?";

                    else if (i < hole)
                        return "Too short\n\nNext Shot?";
            return "Out of bounds\n\nNext Shot?";
        }

        static void Path(double club, int power)
        {
            // Loop through x array
            for (int x = 0; x < 121; x++)
            {
                // Work out y values
                int y = Convert.ToInt32(Math.Round((club * x * (x - power) + 39), 0));
                if (y <= 39 && y > 0)
                    // Display where ball travels
                    grid[y, x] = '*';
            }           

            // Loop through y array
            for (int y = 0; y < 40; y++)

                // Check if valid
                if (((y + (0.25 * club * power * power) - 39) / club) >= 0)
                {
                    // Work out x values
                    int xValue1 = Convert.ToInt32(Math.Round((0.5 * power) + Math.Sqrt((y + (0.25 * club * power * power) - 39) / club), 0));
                    int xValue2 = Convert.ToInt32(Math.Round((0.5 * power) - Math.Sqrt((y + (0.25 * club * power * power) - 39) / club), 0));

                    // Check x values
                    if (xValue1 >= 0 && xValue1 <= 120)
                        grid[y, xValue1] = '*';
                    if (xValue2 >= 0 && xValue2 <= 120)
                        grid[y, xValue2] = '*';
                }
        }
    }
}
