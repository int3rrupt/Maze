using System;
using System.Diagnostics;

namespace Maze
{
    /// <summary>
    /// Maze solving program
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { @"C:\Users\reyes\Downloads\TestImages\maze2.png", @"C:\Users\reyes\Downloads\TestImages\maze2Solution2.png" };
            // Check user input
            if (args.Length != 2)
            {
                Console.WriteLine($"{Resources.Error_MissingArguments}\nEx: {Resources.ArgumentsFormat}");
                return;
            }

            // Keep track of runtime
            Stopwatch stopwatch = Stopwatch.StartNew();
            // Display start time to user
            Console.WriteLine($"Start time: {DateTime.Now.ToString()}");

            // Initialization
            MazeController mazeController = new MazeController();
            bool success = mazeController.SolveMaze(args[0], args[1]);
            stopwatch.Stop();
            TimeSpan duration = stopwatch.Elapsed;
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
