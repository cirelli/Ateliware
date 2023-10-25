using GridMap;

namespace Tests.GridMap
{
    public class CoordinateTests
    {
        [Theory]
        [InlineData("0,0", 0, 0)]
        [InlineData("5,8", 5, 8)]
        [InlineData("100,250", 100, 250)]
        public void NewCoordinateString(string coord, int expectedX, int expectedY)
        {
            var obj = new Coordinate(coord);

            Assert.Equal(expectedX, obj.X);
            Assert.Equal(expectedY, obj.Y);
        }

        [Fact]
        public void NewCoordinateString_InvalidString()
        {
            Assert.Throws<FormatException>(() => { var obj = new Coordinate("a,0"); });
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 8)]
        [InlineData(100, 250)]
        public void NewCoordinate(int x, int y)
        {
            var obj = new Coordinate(x, y);

            Assert.Equal(x, obj.X);
            Assert.Equal(y, obj.Y);
        }

        [Theory]
        [InlineData("0,0", "0,0", true)]
        [InlineData("7,15", "7,15", true)]
        [InlineData("1,1", "0,0", false)]
        [InlineData("1,0", "0,0", false)]
        [InlineData("0,1", "0,0", false)]
        [InlineData("0,0", "1,1", false)]
        [InlineData("0,0", "1,0", false)]
        [InlineData("0,0", "0,1", false)]
        public void TestEquals(string coord1, string coord2, bool isEquals)
        {
            var obj1 = new Coordinate(coord1);
            var obj2 = new Coordinate(coord2);

            Assert.Equal(isEquals, obj1.Equals(obj2));
        }

        [Theory]
        [InlineData("3,3", "3,3", false)]
        [InlineData("3,3", "0,0", false)]
        [InlineData("3,3", "3,6", false)]
        [InlineData("3,3", "6,3", false)]
        [InlineData("3,3", "2,3", false)]
        [InlineData("3,3", "2,2", true)]
        [InlineData("3,3", "3,2", false)]
        [InlineData("3,3", "4,2", true)]
        [InlineData("3,3", "4,3", false)]
        [InlineData("3,3", "4,4", true)]
        [InlineData("3,3", "3,4", false)]
        [InlineData("3,3", "2,4", true)]
        public void Diagonal(string coord1, string coord2, bool isDiagonal)
        {
            var obj1 = new Coordinate(coord1);
            var obj2 = new Coordinate(coord2);

            Assert.Equal(isDiagonal, obj1.IsDiagonal(obj2));
        }

        [Theory]
        [InlineData("0,0", "0,0", 0)]
        [InlineData("0,0", "1,0", 1)]
        [InlineData("0,0", "2,0", 2)]
        [InlineData("0,0", "0,1", 1)]
        [InlineData("0,0", "0,2", 2)]
        [InlineData("0,0", "1,1", 2)]
        [InlineData("0,0", "2,1", 3)]
        [InlineData("0,0", "1,2", 3)]
        public void DistanceTo(string coord1, string coord2, int expectedDistance)
        {
            var obj1 = new Coordinate(coord1);
            var obj2 = new Coordinate(coord2);

            Assert.Equal(expectedDistance, obj1.DistanceTo(obj2));
        }
    }
}
