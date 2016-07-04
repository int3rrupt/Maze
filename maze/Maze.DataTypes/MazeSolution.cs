using Common.DataTypes.Interfaces;

namespace Maze.DataTypes
{
    public struct MazeSolution
    {
        public MazeSolution(bool result, INode lastNode)
        {
            Result = result;
            LastNode = lastNode;
        }

        public bool Result { get; private set; }
        public INode LastNode { get; private set; }
    }
}
