using A_Star;

namespace GridMap
{
    internal class Node : IMapNode
    {
        private readonly Map Map;

        public Node(Coordinate coordinate, Map map)
        {
            Coordinate = coordinate;
            Map = map;
        }

        public Node(string coordinate, Map map)
            : this(new Coordinate(coordinate), map)
        {

        }

        public Coordinate Coordinate { get; set; }

        ICoordinate IMapNode.Coordinate => Coordinate;

        public IEnumerable<IReachableMapNode> GetReachableNodes(IMapNode destination)
            => Map.GetReachableNodes(this, destination);
    }
}
