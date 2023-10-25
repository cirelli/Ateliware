using System.Diagnostics;

namespace A_Star
{
    [DebuggerDisplay("{MapNode.Coordinate} G = {G} H = {H} F = {F}")]
    internal class Node
    {
        protected readonly NodeManager NodeManager;
        public IMapNode MapNode { get; }

        public Node? Parent { get; internal set; }

        /// <summary>
        /// The movement cost to move from the starting point A to a given point on the map, following the path generated to get there.
        /// </summary>
        public int G { get; protected set; } = 0;

        /// <summary>
        /// The estimated movement cost to move from that given point on the map to the final destination, point B.
        /// </summary>
        public int H { get; protected set; }

        /// <summary>
        /// Is calculated by adding G and H
        /// </summary>
        public int F => G + H;

        public EState State { get; set; }

        internal Node(IMapNode mapNode, NodeManager nodeManager)
        {
            MapNode = mapNode;
            NodeManager = nodeManager;
        }

        public IEnumerable<Node> GetReachableNodes(Node destination)
        {
            var reachableNodes = MapNode.GetReachableNodes(destination.MapNode);

            foreach (var reachableNode in reachableNodes)
            {
                Node node = NodeManager.GetOrCreateNode(reachableNode.MapNode);

                if (node.State == EState.Closed) continue;

                int tempG = this.G + reachableNode.CostToMove;

                if (node.State == EState.Open && tempG < node.G)
                {
                    node.Parent = this;
                    node.G = tempG;
                }

                if (node.State == EState.Untested)
                {
                    node.State = EState.Open;
                    node.Parent = this;
                    node.G = tempG;
                    node.H = reachableNode.CostToMoveToDestination;
                }

                yield return node;
            }
        }
    }
}
