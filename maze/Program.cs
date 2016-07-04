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
            // Degugging
            //args = new string[] { @"C:\Users\reyes\Downloads\TestImages\maze1.png", @"C:\Users\reyes\Downloads\TestImages\mazeDebugSolution.png" };

            // Display start time to user
            Console.WriteLine($"Start time: {DateTime.Now.ToString()}\n");

            string message = string.Empty;
            try
            {
                // Check user input
                if (args.Length != 2)
                {
                    message = $"{Resources.Error_MissingArguments}\nEx: {Resources.ArgumentsFormat}\n";
                }
                else
                {
                    // Keep track of runtime
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    // Initialization
                    MazeController mazeController = new MazeController();
                    // Try to solve the maze
                    bool success = mazeController.SolveMaze(args[0], args[1]);
                    // Determine runtime
                    stopwatch.Stop();
                    TimeSpan duration = stopwatch.Elapsed;
                    // Append success or failure message
                    message += success ? $"Maze solved successfully!. Solution saved to: {args[1]}\n" : "Maze could not be sovled.\n";
                    // Append runtime message
                    message += $"Runtime: {duration}\n";
                }
            }
            catch (Exception ex)
            {
                message += $"An error has occurred:\n{ex.Message}\n";
            }
            // Write any messages
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
    }
}
