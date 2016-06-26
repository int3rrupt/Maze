using System;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Start time: {startTime.ToString()}");
            // DEBUG
            args = new string[] { @"C:\Users\reyes\Downloads\TestImages\maze3.png", $"C:\\Users\\reyes\\Downloads\\TestImages\\mazeSolution_{startTime.ToString("HH_mm_ss")}.png" };


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
