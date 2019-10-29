using System;
using System.Linq;
using MartianRobots.Models;

namespace MartianRobots.Extensions
{
    public static class RobotExtensions
    {
        public static bool IsOutOfBound(this Surface suface, Coordinate coordinate)
        {
            return suface.BottomLeft.X > coordinate.X
                   || suface.BottomLeft.Y > coordinate.Y
                   || suface.TopRight.X < coordinate.X
                   || suface.TopRight.Y < coordinate.Y;
        }

        public static Coordinate NextCoordinate(this Surface surface, Coordinate coordinate, Orientations orientation,
            int stepCount)
        {
            Coordinate result;
            switch (orientation)
            {
                case Orientations.E:
                    result = new Coordinate(coordinate.X + stepCount, coordinate.Y);
                    break;
                case Orientations.N:
                    result = new Coordinate(coordinate.X, coordinate.Y + stepCount);
                    break;
                case Orientations.S:
                    result = new Coordinate(coordinate.X, coordinate.Y - stepCount);
                    break;
                case Orientations.W:
                    result = new Coordinate(coordinate.X - stepCount, coordinate.Y);
                    break;
                default:
                    result = coordinate;
                    break;
            }

            return result;
        }

        public static void Turn(this Robot robot, Turns turn)
        {
            var o = (4 + (int) robot.Orientation + (int) turn) % 4;
            robot.Orientation = (Orientations) Enum.ToObject(typeof(Orientations), o);
        }

        public static void Move(this Robot robot, Coordinate coordinate)
        {
            robot.Position = coordinate;
        }

        public static bool IsScentedCoordinate(this Surface suface, Coordinate coordinate)
        {
            return suface.ScentedCoordinates.Any(c => c.X == coordinate.X && c.Y == coordinate.Y);
        }

        public static void AddScentedCoordinate(this Surface suface, Coordinate coordinate)
        {
            suface.ScentedCoordinates.Add(coordinate);
        }
    }
}