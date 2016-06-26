using System;
using System.IO;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Start time: {startTime.ToString()}");
            // DEBUG
            if (args.Length == 0)
                args = new string[] { @"C:\Users\reyes\Downloads\TestImages\maze3.png", $"C:\\Users\\reyes\\Downloads\\TestImages\\mazeSolution_{startTime.ToString("HH_mm_ss")}.png" };
            //args = new string[] { @"C:\Users\reyes\Downloads\TestImages\maze3.png"};

            //string output = string.Empty;
            //if (args.Length == 1 || args.Length == 2)
            //{
            //    if (args.Length == 1)
            //        output = $"{Path.GetDirectoryName(args[0])} {Path.GetFileNameWithoutExtension(args[0])}_{startTime.ToString("hh_mm_ss")}.{Path.GetExtension(args[0])}";
            //}
            //switch()

            // Check user input
            if (args.Length != 2)
            {
                Console.WriteLine($"{Resources.Error_MissingArguments}\nEx: {Resources.ArgumentsFormat}");
                return;
            }

            // Initialization
            MazeController mazeController = new MazeController();
            bool success = mazeController.SolveMaze(args[0], args[1]);
            TimeSpan duration = new TimeSpan(DateTime.Now.Ticks - startTime.Ticks);
            // Try to solve the maze
            if (success)
                Console.WriteLine($"Maze solved successfully!. Solution saved to: {args[1]}");
            else
                Console.WriteLine("Maze could not be sovled.");
            Console.WriteLine($"Runtime: {duration}");
            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
    }
}
