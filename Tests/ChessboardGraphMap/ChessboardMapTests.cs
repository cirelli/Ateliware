using ChessboardGraphMap;

namespace Tests.ChessboardGraphMap
{
    public class ChessboardMapTests
    {
        public static IEnumerable<object[]> NewChessboardMapData()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            yield return new object[] { "{\"A1\":{\"A2\":11.88},\"A2\":{\"A1\":21.77}}", new List<Location> {
                new Location("A1", null) { Roads = { new Road(new Location("A2", null), 11.88m) } },
                new Location("A2", null) { Roads = { new Road(new Location("A1", null), 21.77m) } }
            } };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Theory]
        [MemberData(nameof(NewChessboardMapData))]
        public void NewChessboardMap(string json, object expectedLocations)
        {
            var obj = new ChessboardMap(json);

            foreach (var item in (List<Location>)expectedLocations)
            {
                Assert.True(obj.Locations.TryGetValue(item.Coordinate, out var location));

                foreach (var expectedRoad in item.Roads)
                {
                    var road = location.Roads.FirstOrDefault(q => q.To.Coordinate.Equals(expectedRoad.To.Coordinate));
                    Assert.NotNull(road);

                    Assert.Equal(expectedRoad.TimeToCross, road.TimeToCross);
                }
            }
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("A2")]
        public void GetNode(string coord)
        {
            var map = new ChessboardMap("{\"A1\":{\"A2\":11.88},\"A2\":{\"A1\":21.77}}");
            var node = map.GetNode(coord);

            Assert.NotNull(node);
            Assert.Equal(coord, node.Coordinate.ToString());
        }

        [Fact]
        public void GetNode_InvalidCoord()
        {
            var coord = "B1";
            var map = new ChessboardMap("{\"A1\":{\"A2\":11.88},\"A2\":{\"A1\":21.77}}");

            Assert.Throws<ArgumentOutOfRangeException>(() => map.GetNode(coord));
        }

        [Theory]
        [InlineData("A1", new string[] { "A2" })]
        [InlineData("A2", new string[] { "A1", "A3" })]
        [InlineData("A3", new string[] { "A2" })]
        public void GetReachableNodes(string coord, string[] expectedCoords)
        {
            var map = new ChessboardMap("{\"A1\":{\"A2\":11.88},\"A2\":{\"A1\":21.77,\"A3\":10},\"A3\":{\"A2\":15}} ");
            var node = map.GetNode(coord);
            var reachableCoords = map.GetReachableNodes(node, node).Select(q => q.MapNode.Coordinate).ToList();

            foreach (var item in expectedCoords)
            {
                Assert.Contains(new Coordinate(item), reachableCoords);
            }
        }
    }
}
