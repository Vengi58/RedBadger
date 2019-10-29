using System.Collections.Generic;
using MartianRobots.Models;

namespace MartianRobots.Controllers
{
    public interface IRobotController
    {
        Surface Surface { get; }
        List<RobotRunBook> RobotRunbooks { get; }

        IEnumerable<string> RunRobots();
    }
}