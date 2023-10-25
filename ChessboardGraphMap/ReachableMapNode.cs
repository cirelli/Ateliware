using A_Star;

namespace ChessboardGraphMap
{
    internal class ReachableMapNode : IReachableMapNode
    {
        public ReachableMapNode(IMapNode mapNode)
        {
            MapNode = mapNode;
        }

        public IMapNode MapNode { get; }

        public int CostToMove { get; set; }

        public int CostToMoveToDestination { get; set; }
    }
}
