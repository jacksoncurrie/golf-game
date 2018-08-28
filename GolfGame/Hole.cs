using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame
{
    class Hole
    {
        public int StartingHeight { get; private set; }
        public int HoleXPos { get; private set; }
        public int HoleYPos { get; private set; }

        public Hole (int startingHeight, int holeYPos)
        {
            // Set hole attributes
            StartingHeight = startingHeight;
            HoleYPos = holeYPos;

            // Random hole position
            Random random = new Random();
            HoleXPos = random.Next(50, 120);
        }
    }
}
