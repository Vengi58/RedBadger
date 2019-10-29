using System;
using System.Collections.Generic;
using MartianRobots.Extensions;
using MartianRobots.Models;

namespace MartianRobots.Controllers
{
    public class RobotController : IRobotController
    {
        public RobotController(Surface surface, List<RobotRunBook> robotRunBooks)
        {
            Surface = surface ?? throw new ArgumentNullException(nameof(surface));
            RobotRunbooks = robotRunBooks ?? throw new ArgumentNullException(nameof(robotRunBooks));
        }

        public Surface Surface { get; }
        public List<RobotRunBook> RobotRunbooks { get; }

        public IEnumerable<string> RunRobots()
        {
            foreach (var runBook in RobotRunbooks)
            {
                var robot = new Robot(runBook.Position, runBook.Orientation);
                var instructions = runBook.Instructions.GetEnumerator();
                while (robot.Status == Statuses.LIVE && instructions.MoveNext())
                {
                    var instr = instructions.Current.ToString();
                    Enum.TryParse(instr, out Turns turn);
                    Enum.TryParse(instr, out Moves move);
                    if (instr.Equals(turn.ToString()))
                    {
                        robot.Turn(turn);
                    }
                    else if (instr.Equals(move.ToString()))
                    {
                        var next = Surface.NextCoordinate(robot.Position, robot.Orientation, 1);
                        if (Surface.IsOutOfBound(next))
                        {
                            if (Surface.IsScentedCoordinate(robot.Position)) continue;
                            robot.Status = Statuses.LOST;
                            Surface.AddScentedCoordinate(robot.Position);
                        }
                        else
                        {
                            robot.Move(next);
                        }
                    }
                }

                var status = robot.Status == Statuses.LOST ? robot.Status.ToString() : string.Empty;
                yield return $"{robot.Position.X} {robot.Position.Y} {robot.Orientation} {status}";
            }
        }
    }
}