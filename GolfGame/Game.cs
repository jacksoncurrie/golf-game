using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame
{
    class Game
    {
        public int Power { get; set; }
        public double Club { get; set; }
        public bool HasWon { get; set; }
        public int CurrentHoleNum { get; set; }
        public int[] Scores { get; set; }

        Hole currentHole;
        List<Hole> course;
        char[,] grid;

        public Game()
        {
            // Set course
            course = new List<Hole>
            {
                new Hole("Flat", 39, 39),
                new Hole("Moving Up", 33, 39),
                new Hole("Up Higher", 28, 39),
                new Hole("Hole Moving Up", 28, 33),
                new Hole("Hole Up Higher", 28, 28),
                new Hole("Hole Above Me", 30, 22),
                new Hole("Hidden ball?", 3, 3),
                new Hole("Don't Go Out", 25, 35),
                new Hole("Big Drop", 5, 39)
            };

            // Set starting hole
            CurrentHoleNum = 0;

            Scores = new int[9];

            Console.Write
            (
                "======================\n" +
                "===   Start Game   ===\n" +
                "======================\n" +
                "\n" +
                "Nine hole golf game. Get the ball in the hole.\n" +
                "\n" +
                "Press any key to start game..."
            );

            Console.ReadKey();
            Console.Clear();
        }

        public void StartHole()
        {
            HasWon = false;
            currentHole = course[CurrentHoleNum];

            // Random hole position
            Random random = new Random();
            currentHole.HoleXPos = random.Next(50, 108);

            if (currentHole == course[7])
            {
                currentHole.HoleXPos = 120;
                currentHole.HoleYPos = random.Next(30, 35);
            }
            else if (currentHole == course[8])
            {
                currentHole.HoleXPos = 120;
                currentHole.StartingHeight = random.Next(3, 8);
            }

            Console.Write
            (
                $"===================\n" +
                $" {currentHole.Name}\n" +
                $"===================\n" +
                $"\n" +
                $"Press any key to start hole..."
            );

            Console.ReadKey();

            Scores[CurrentHoleNum] = 0;
            ResetHole();
            DisplayHole();
        }

        void ResetHole()
        {
            grid = new char[40, 121];

            // Set flag
            grid[currentHole.HoleYPos - 2, currentHole.HoleXPos - 1] = '<';
            grid[currentHole.HoleYPos - 2, currentHole.HoleXPos] = '|';
            grid[currentHole.HoleYPos - 1, currentHole.HoleXPos] = '|';
            grid[currentHole.HoleYPos, currentHole.HoleXPos] = '|';
            for (int i = 39; i > currentHole.HoleYPos; i--)
                grid[i, currentHole.HoleXPos] = '#';

            // Set player
            grid[currentHole.StartingHeight, 0] = '*';
            for (int h = 39; h > currentHole.StartingHeight; h--)
                grid[h, 0] = '#';
        }

        public void DisplayHole()
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

        public void Path()
        {
            ResetHole();
            Scores[CurrentHoleNum]++;

            // Loop through y array
            for (int y = 0; y < 40; y++)

                // Check if valid
                if (((y + (0.25 * Club * Power * Power) - currentHole.StartingHeight) / Club) >= 0)
                {
                    // Work out x values
                    int xValue1 = Convert.ToInt32(Math.Round((0.5 * Power) + Math.Sqrt((y + (0.25 * Club * Power * Power) - currentHole.StartingHeight) / Club), 0));
                    int xValue2 = Convert.ToInt32(Math.Round((0.5 * Power) - Math.Sqrt((y + (0.25 * Club * Power * Power) - currentHole.StartingHeight) / Club), 0));

                    // Check x values
                    if (xValue1 >= 0 && xValue1 <= 120)
                        grid[y, xValue1] = '*';
                    if (xValue2 >= 0 && xValue2 <= 120)
                        grid[y, xValue2] = '*';
                }

            // Loop through x array
            for (int x = 0; x < 121; x++)
            {
                // Work out y values
                int y = Convert.ToInt32(Math.Round((Club * x * (x - Power) + currentHole.StartingHeight), 0));
                if (y <= 39 && y >= 0)
                    // Display where ball travels
                    grid[y, x] = '*';
            }

            DisplayHole();
            Console.Write(CheckLanding());
            Console.ReadKey();
            Console.Clear();
            ResetHole();
            DisplayHole();
        }

        public string CheckLanding()
        {
            // Check where ball lands
            if (grid[currentHole.HoleYPos, currentHole.HoleXPos] == '*')
            {
                HasWon = true;
                return "In the Hole!..";
            }

            if (Scores[CurrentHoleNum] >= 20)
            {
                HasWon = true;
                return "Too many shots...";
            }

            // Loop through end column
            if (grid[39, 120] != '*')
            {
                for (int j = 0; j <= 38; j++)
                {
                    if (grid[j, 120] == '*')
                    {
                        Scores[CurrentHoleNum]++;
                        return "Out of bounds, two shot penatly\n\nNext Shot..";
                    }
                }
            }

            // Loop through bottom row
            for (int i = 120; i > 0; i--)
            {
                // Check for location of ball
                if (grid[currentHole.HoleYPos, i] == '*' && i > currentHole.HoleXPos)
                {
                    return "Too far\n\nNext Shot...";
                }
            }

            return "Too short\n\nNext Shot...";
        }

        public void DisplayScoreCard()
        {
            Console.Write
            (
                "+----+----+----+----+----+----+----+----+----+-------+\n" +
                "|  1 |  2 |  3 |  4 |  5 |  6 |  7 |  8 |  9 | Total |\n" +
                "+----+----+----+----+----+----+----+----+----+-------+\n" +
                "|"
            );

            for (int i = 0; i < 9; i++)
            {
                string score = Scores[i] == 0 ? "  " : Convert.ToString(Scores[i]);
                Console.Write($" {score, 2} |");
            }

            Console.WriteLine($"  {Scores.Sum(), 3}  |\n+----+----+----+----+----+----+----+----+----+-------+");

            // Wait for input
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
