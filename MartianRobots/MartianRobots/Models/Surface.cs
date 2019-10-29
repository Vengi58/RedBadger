using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Models
{
    public class Surface
    {
        public Surface(Coordinate topRight)
        {
            BottomLeft = new Coordinate {X = 0, Y = 0};
            TopRight = topRight;
            ScentedCoordinates = new HashSet<Coordinate>();
        }

        public Coordinate BottomLeft { get; }
        public Coordinate TopRight { get; set; }
        public HashSet<Coordinate> ScentedCoordinates { get; }
    }

    public struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}