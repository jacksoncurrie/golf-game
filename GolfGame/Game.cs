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

        Hole currentHole;
        List<Hole> course;
        char[,] grid;
        int shots;

        public Game()
        {
            // Set course
            course = new List<Hole>()
            {
                new Hole(30, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39),
                new Hole(39, 39)
            };

            // Set starting hole
            CurrentHoleNum = 0;
        }

        public void StartHole()
        {
            currentHole = course[CurrentHoleNum];
            shots = 0;
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
            shots++;

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
        }

        public string CheckLanding()
        {
            int occurrence = 0;
            // Loop through bottom row
            for (int i = 120; i > 0; i--)

                // Check for location of ball
                if (grid[currentHole.HoleYPos, i] == '*')
                {
                    occurrence++;

                    // Check where ball lands
                    if (i == currentHole.HoleXPos)
                    {
                        HasWon = true;
                        return "You won!\n\n";
                    }
                    else if (i > currentHole.HoleXPos)
                        return "Too far\n\nNext Shot?";

                    else if (i < currentHole.HoleXPos && occurrence == 2 )
                        return "Too short\n\nNext Shot?";
                }

            return "Out of bounds\n\nNext Shot?";
        }
    }
}
