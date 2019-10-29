using System;
using System.Collections.Generic;
using System.IO;
using MartianRobots.Controllers;
using MartianRobots.Models;

namespace MartianRobots
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
                return;

            var parsedInstructionsFile = ParseInstructionsFile(args[0]);
            var surface = parsedInstructionsFile.Item1;
            var robotRunBook = parsedInstructionsFile.Item2;
            var robotController = new RobotController(surface, robotRunBook);

            foreach (var runResult in robotController.RunRobots()) Console.WriteLine(runResult);
        }

        public static Tuple<Surface, List<RobotRunBook>> ParseInstructionsFile(string filePath)
        {
            int maxCoordinate = 50;
            int maxCharactersLength = 100;

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            var file = new StreamReader(filePath);
            var topRight = file.ReadLine();
            if (string.IsNullOrWhiteSpace(topRight) || topRight.Length != 2)
                throw new ArgumentException(nameof(filePath));
            var surface = new Surface(new Coordinate(Convert.ToInt32(topRight[0].ToString()),
                Convert.ToInt32(topRight[1].ToString())));

            string line;
            var starts = new List<RobotRunBook>();
            while ((line = file.ReadLine()) != null)
            {
                var firstLine = line;
                if (Convert.ToInt32(firstLine[0].ToString()) > maxCoordinate 
                    || Convert.ToInt32(firstLine[1].ToString()) > maxCoordinate)
                    throw new ArgumentException();

                var secondLine = file.ReadLine();
                if (string.IsNullOrWhiteSpace(firstLine)
                    || firstLine.Length > 5
                    || firstLine.Length < 2)
                    throw new ArgumentException(nameof(filePath));

                if (string.IsNullOrWhiteSpace(secondLine)
                    || secondLine.Length >= maxCharactersLength
                    || secondLine.Length < 1)
                    throw new ArgumentException(nameof(filePath));

                starts.Add(new RobotRunBook
                {
                    Position = new Coordinate(Convert.ToInt32(firstLine[0].ToString()),
                        Convert.ToInt32(firstLine[1].ToString())),
                    Orientation = Enum.Parse<Orientations>(firstLine[2].ToString()),
                    Instructions = secondLine
                });
            }

            file.Close();
            return new Tuple<Surface, List<RobotRunBook>>(surface, starts);
        }
    }
}