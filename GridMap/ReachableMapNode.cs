using A_Star;

namespace GridMap
{
    internal class ReachableMapNode : IReachableMapNode
    {
        public ReachableMapNode(Node node)
        {
            MapNode = node;
        }

        public IMapNode MapNode { get; }

        public int CostToMove { get; set; }

        public int CostToMoveToDestination { get; set; }
    }
}
