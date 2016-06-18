namespace maze
{
    public class Maze
    {
        #region Constructor

        public Maze()
        {
        }

        public Maze(char[][] mazeDefinition, int startLocation, int finishLocation)
        {
            this.MazeDefinition = mazeDefinition;
            this.StartLocation = startLocation;
            this.FinishLocation = finishLocation;
        }

        #endregion

        #region Properties

        public char[][] MazeDefinition { get; set; }

        public int StartLocation { get; set; }

        public int FinishLocation { get; set; }

        #endregion
    }
}
