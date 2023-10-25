using A_Star;

namespace ChessboardGraphMap
{
    internal class Location : IMapNode
    {
        private readonly ChessboardMap Map;

        public Location(string coordinate, ChessboardMap map)
        {
            Coordinate = new Coordinate(coordinate);
            Map = map;
        }

        public Coordinate Coordinate { get; }

        ICoordinate IMapNode.Coordinate => Coordinate;

        public List<Road> Roads { get; set; } = new();

        public override string ToString()
            => Coordinate.ToString();

        public override bool Equals(object? obj)
        {
            if (obj is not Location o) return base.Equals(obj);

            return Coordinate.Equals(o.Coordinate);
        }

        public override int GetHashCode()
            => Coordinate.GetHashCode();

        IEnumerable<IReachableMapNode> IMapNode.GetReachableNodes(IMapNode destination)
            => ((IMap)Map).GetReachableNodes(this, destination);
    }
}
