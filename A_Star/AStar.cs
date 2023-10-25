namespace A_Star
{
    public class AStar
    {
        private readonly IMap Map;

        private NodeManager? NodeManager { get; set; }
        private List<Node>? OpenList { get; set; }

        public AStar(IMap map)
        {
            Map = map;
        }

        private void Initialize()
        {
            NodeManager = new NodeManager(Map);
            OpenList = new List<Node>();
        }

        public PathResult FindPath(string coordStart, string coordEnd)
        {
            Initialize();

            Node nodeStart = NodeManager!.GetOrCreateNode(coordStart);
            OpenList!.Add(nodeStart);

            Node nodeEnd = NodeManager.GetOrCreateNode(coordEnd);

            Node pathNode = FindPath(nodeStart, nodeEnd);
            Stack<string> stack = new();

            var result = new PathResult
            {
                TotalCost = pathNode.F
            };

            while (pathNode.Parent != null)
            {
                stack.Push($"{pathNode.Parent.MapNode.Coordinate}-{pathNode.MapNode.Coordinate}");
                pathNode = pathNode.Parent;
            }

            result.Path = string.Join("+", stack);

            return result;
        }

        private Node FindPath(Node nodeStart, Node nodeEnd)
        {
            nodeStart.State = EState.Closed;
            OpenList!.Remove(nodeStart);

            List<Node> reachableNodes = nodeStart.GetReachableNodes(nodeEnd).ToList();
            OpenList.AddRange(reachableNodes);

            Node? nextNode = GetNearNode();

            if (nextNode == null) throw new Exception("No path found");

            if (nextNode.MapNode.Coordinate.Equals(nodeEnd.MapNode.Coordinate))
            {
                return nextNode;
            }

            return FindPath(nextNode, nodeEnd);
        }

        private Node? GetNearNode()
        {
            OpenList = OpenList!.Where(q => q.State != EState.Closed).OrderBy(q => q.H).ThenBy(q => q.F).ToList();
            return OpenList.FirstOrDefault();
        }
    }
}
