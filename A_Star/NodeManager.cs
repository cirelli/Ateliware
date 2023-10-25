namespace A_Star
{
    internal class NodeManager
    {
        private readonly IMap Map;
        private readonly Dictionary<ICoordinate, Node> Nodes = new();

        public NodeManager(IMap map)
        {
            Map = map;
        }

        internal Node GetOrCreateNode(string coordinate)
        {
            var mapNode = Map.GetNode(coordinate);
            return GetOrCreateNode(mapNode);
        }

        internal Node GetOrCreateNode(IMapNode mapNode)
        {
            Nodes.TryGetValue(mapNode.Coordinate, out var node);

            if (node == null)
            {
                node = new Node(mapNode, this);
                Nodes.Add(mapNode.Coordinate, node);
            }

            return node;
        }
    }
}
