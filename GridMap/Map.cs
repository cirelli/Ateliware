using A_Star;

namespace GridMap
{
    internal class Map : IMap
    {
        private const int CostToMove = 10;
        private const int CostToMoveDiagonal = 14;
        private readonly bool[,] Grid;

        public Map(bool[,] grid)
        {
            Grid = grid;
        }

        public IMapNode GetNode(string coordinate)
        {
            var node = new Node(coordinate, this);
            _ = Grid[node.Coordinate.Y, node.Coordinate.X];

            return node;
        }

        private static List<Coordinate> GetAdjacent(Coordinate coordinate)
            => new()
            {
                new Coordinate(coordinate.X - 1, coordinate.Y),
                new Coordinate(coordinate.X - 1, coordinate.Y - 1),
                new Coordinate(coordinate.X,     coordinate.Y - 1),
                new Coordinate(coordinate.X + 1, coordinate.Y - 1),
                new Coordinate(coordinate.X + 1, coordinate.Y),
                new Coordinate(coordinate.X + 1, coordinate.Y + 1),
                new Coordinate(coordinate.X,     coordinate.Y + 1),
                new Coordinate(coordinate.X - 1, coordinate.Y + 1)
            };

        private bool IsValidCoord(Coordinate coordinate)
            => coordinate.X >= 0 && coordinate.X < Grid.GetLength(1) && coordinate.Y >= 0 && coordinate.Y < Grid.GetLength(0);

        public IEnumerable<IReachableMapNode> GetReachableNodes(IMapNode start, IMapNode destination)
        {
            if (start is not Node nodeStart) throw new ArgumentException("Invalid parameter.", nameof(start));
            if (destination is not Node nodeDestination) throw new ArgumentException("Invalid parameter.", nameof(destination));

            var coords = GetAdjacent(nodeStart.Coordinate);

            foreach (var coord in coords)
            {
                if (!IsValidCoord(coord)) continue;
                if (!Grid[coord.Y, coord.X]) continue;

                int costToMove = CostToMove;
                if (coord.IsDiagonal(nodeStart.Coordinate))
                {
                    costToMove = CostToMoveDiagonal;
                }

                yield return new ReachableMapNode(new(coord, this))
                {
                    CostToMove = costToMove,
                    CostToMoveToDestination = coord.DistanceTo(nodeDestination.Coordinate) * CostToMove,
                };
            }
        }
    }
}