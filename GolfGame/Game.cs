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

        int currentHoleNum;
        Hole currentHole;
        List<Hole> course;
        char[,] grid;
        int shots;

        public Game()
        {
            // Set course
            course = new List<Hole>()
            {
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0),
                new Hole(0, 0)
            };

            // Set starting hole
            currentHoleNum = 0;
            currentHole = course[currentHoleNum];

            StartHole();
        }

        void StartHole()
        {
            shots = 0;
            grid = new char[40, 121];
            DisplayHole();
        }

        void DisplayHole()
        {
            Console.Clear();

            // Set flag
            grid[currentHole.HoleYPos - 2, currentHole.HoleXPos - 1] = '<';
            grid[currentHole.HoleYPos - 2, currentHole.HoleXPos] = '|';
            grid[currentHole.HoleYPos - 1, currentHole.HoleXPos] = '|';
            grid[currentHole.HoleYPos, currentHole.HoleXPos] = '|';
            for (int i = 0; i < currentHole.HoleYPos; i++)
                grid[i, currentHole.HoleXPos] = '#';

            // Set player
            grid[currentHole.StartingHeight, 0] = '*';
            for(int h = 0; h < currentHole.StartingHeight; h++)
                grid[h, 0] = '#';

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
            Console.WriteLine("#^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
