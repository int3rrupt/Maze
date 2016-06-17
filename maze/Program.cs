using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze
{
    class Program
    {
        static void Main(string[] args)
        {
            // DEBUG
            args = new string[] { @"C:\Users\reyes\Downloads\maze4.png", @"C:\Users\reyes\Downloads\mazeSolution.png" };




            // Check user input
            if (args.Length != 2)
            {
                Console.WriteLine($"{Resources.Error_MissingArguments}\nEx: {Resources.ArgumentsFormat}");
                return;
            }

            // Initialization
            MazeController mazeController = new MazeController();

            // Try to solve the maze
            if (mazeController.SolveMaze(args[0], args[1]))
                Console.WriteLine($"Maze solved successfully!. Solution saved to: {args[2]}");
            else
                Console.WriteLine("Maze could not be sovled.");
        }
    }
}
