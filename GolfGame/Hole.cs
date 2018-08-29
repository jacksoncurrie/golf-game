using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame
{
    class Hole
    {
        public string Name { get; set; }
        public int StartingHeight { get; set; }
        public int HoleXPos { get; set; }
        public int HoleYPos { get; set; }

        public Hole (string name, int startingHeight, int holeYPos)
        {
            // Set hole attributes
            Name = name;
            StartingHeight = startingHeight;
            HoleYPos = holeYPos;
        }
    }
}
