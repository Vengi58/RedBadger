using System;

namespace MartianRobots.Models
{
    public class Robot
    {
        public Robot(Coordinate position, Orientations orientation)
        {
            Position = position;
            Orientation = orientation;
        }

        public Coordinate Position { get; set; }
        public Orientations Orientation { get; set; }
        public Statuses Status { get; set; }
    }
}