using GridMap;

namespace Tests.GridMap
{
    public class MapTests
    {
        public static IEnumerable<object[]> GetNodeData()
        {
            yield return new object[] { new bool[,] { { true } }, "0,0" };
            yield return new object[] { new bool[,] { { false } }, "0,0" };
            yield return new object[] { new bool[,] { { true, true }, { true, true } }, "1,1" };
        }

        [Theory]
        [MemberData(nameof(GetNodeData))]
        public void GetNode(bool[,] grid, string coordinate)
        {
            var map = new Map(grid);
            var node = map.GetNode(coordinate);

            Assert.Equal(new Coordinate(coordinate), node.Coordinate);
        }

        [Fact]
        public void GetNode_InvalidCoordinate()
        {
            var map = new Map(new bool[,] { { true } });
            Assert.Throws<IndexOutOfRangeException>(() => map.GetNode("0,1"));
        }

        public static IEnumerable<object[]> GetReachableNodesData()
        {
            yield return new object[] { "0,0", new string[] { "1,0", "0,1", "1,1" } };
            yield return new object[] { "0,1", new string[] { "0,0", "1,0", "1,1", "0,2", "1,2" } };
            yield return new object[] { "2,1", new string[] { "1,0", "2,0", "1,1", "3,1", "1,2", "2,2" } };
            yield return new object[] { "1,1", new string[] { "0,0", "1,0", "2,0", "0,1", "2,1", "0,2", "1,2", "2,2" } };
        }

        [Theory]
        [MemberData(nameof(GetReachableNodesData))]
        public void GetReachableNodes(string startCoord, string[] expectedCoords)
        {
            var grid = new bool[,]
            {
                { true, true, true, false },
                { true, true, true, true },
                { true, true, true, false },
            };
            var map = new Map(grid);
            var node = new Node(new Coordinate(startCoord), map);
            var reachableCoords = map.GetReachableNodes(node, node).Select(q => q.MapNode.Coordinate).ToList();

            foreach (var item in expectedCoords)
            {
                Assert.Contains(new Coordinate(item), reachableCoords);
            }
        }
    }
}
