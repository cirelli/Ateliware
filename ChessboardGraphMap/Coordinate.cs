using A_Star;

namespace ChessboardGraphMap
{
    internal class Coordinate : ICoordinate
    {
        readonly char letterX;

        public Coordinate(string coordinate)
        {
            letterX = char.ToUpper(coordinate[0]);

            X = letterX - 'A';
            Y = Convert.ToInt32(coordinate[1].ToString());
        }

        public int X { get; }

        public int Y { get; }

        public override string ToString()
            => string.Concat(letterX, Y);

        public override bool Equals(object? obj)
        {
            return Equals(obj as ICoordinate);
        }

        public bool Equals(ICoordinate? other)
        {
            if (other is not Coordinate obj) return base.Equals(other);

            return obj != null && X.Equals(obj.X) && Y.Equals(obj.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        internal int DistanceTo(Coordinate coordinate)
        {
            return Math.Abs(X - coordinate.X) + Math.Abs(Y - coordinate.Y);
        }
    }
}
