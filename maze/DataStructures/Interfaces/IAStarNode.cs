namespace Common.DataStructures.Interfaces
{
    public interface IAStarNode : INode
    {
        int G { get; set; }

        int H { get; set; }
    }
}
