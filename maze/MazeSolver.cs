using Common.Algorithms;
using Common.DataTypes.Interfaces;
using Maze.DataTypes;
using System.Drawing;

namespace Maze
{
    /// <summary>
    /// A maze solving class.
    /// </summary>
    public static class MazeSolver
    {
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startColor"></param>
        /// <param name="finishColor"></param>
        /// <returns></returns>
        public static MazeSolution SolveMaze<T>(MazeGraph graph) where T : IAStarNode, new()
        {
            return SearchAlgorithms.AStar<T>(graph);
        }

        #endregion
    }
}
