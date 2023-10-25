using ChessboardGraphMap;

namespace Tests.ChessboardGraphMap
{
    public class CoordinateTests
    {
        [Theory]
        [InlineData("A1", 0, 1)]
        [InlineData("a1", 0, 1)]
        [InlineData("H8", 7, 8)]
        public void NewCoordinateString(string coord, int expectedX, int expectedY)
        {
            var obj = new Coordinate(coord);

            Assert.Equal(expectedX, obj.X);
            Assert.Equal(expectedY, obj.Y);
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("a1")]
        [InlineData("H8")]
        public void TestToString(string coord)
        {
            var obj = new Coordinate(coord);

            Assert.Equal(coord.ToUpper(), obj.ToString());
        }

        [Theory]
        [InlineData("A1", "A1", true)]
        [InlineData("A1", "a1", true)]
        [InlineData("A1", "B1", false)]
        [InlineData("A1", "A2", false)]
        public void TestEquals(string coord1, string coord2, bool isEquals)
        {
            var obj1 = new Coordinate(coord1);
            var obj2 = new Coordinate(coord2);

            Assert.Equal(isEquals, obj1.Equals(obj2));
        }

        [Theory]
        [InlineData("A1", "A1", 0)]
        [InlineData("A1", "a1", 0)]
        [InlineData("A1", "B1", 1)]
        [InlineData("A1", "A2", 1)]
        [InlineData("A1", "B2", 2)]
        [InlineData("A1", "H8", 14)]
        public void DistanceTo(string coord1, string coord2, int expected)
        {
            var obj1 = new Coordinate(coord1);
            var obj2 = new Coordinate(coord2);

            Assert.Equal(expected, obj1.DistanceTo(obj2));
        }
    }
}
