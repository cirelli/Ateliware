using A_Star;

namespace GridMap
{
    internal class Coordinate : ICoordinate
    {
        public Coordinate(string coordinate)
        {
            var coord = coordinate.Split(',');

            X = Convert.ToInt32(coord[0].ToString());
            Y = Convert.ToInt32(coord[1].ToString());
        }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        public override string ToString()
            => $"({X},{Y})";

        public bool Equals(ICoordinate? other)
        {
            if (other is not Coordinate obj) return base.Equals(other);

            return X.Equals(obj.X) && Y.Equals(obj.Y);
        }

        public override int GetHashCode()
            => HashCode.Combine(X, Y);

        internal bool IsDiagonal(Coordinate coordinate)
            => (coordinate.X == X + 1 || coordinate.X == X - 1) && (coordinate.Y == Y + 1 || coordinate.Y == Y - 1);

        internal int DistanceTo(Coordinate coordinate)
            => Math.Abs(coordinate.X - X) + Math.Abs(coordinate.Y - Y);
    }
}
