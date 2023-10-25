using A_Star;
using GridMap;

using Node = A_Star.Node;

namespace Tests.AStar
{
    public class NodeTests
    {
        private readonly Map Map = new(new bool[,] {
                {true, true, true, true, true, true, true},
                { true, true, true, false, true, true, true},
                {true, true, true, false, true, true, true},
                {true, true, true, false, true, true, true},
                {true, true, true, true, true, true, true}
            });

        [Fact]
        public void GetReachableNodes()
        {
            var manager = new NodeManager(Map);
            var startNode = manager.GetOrCreateNode("1,2");
            var endNode = manager.GetOrCreateNode("5,2");
            var reachableNodes = startNode.GetReachableNodes(endNode).ToList();

            var expectedNodes = new[]
            {
                new { Coordinate = new Coordinate("0,1"), G = 14, H = 60 },
                new { Coordinate = new Coordinate("1,1"), G = 10, H = 50 },
                new { Coordinate = new Coordinate("2,1"), G = 14, H = 40 },
                new { Coordinate = new Coordinate("0,2"), G = 10, H = 50 },
                new { Coordinate = new Coordinate("2,2"), G = 10, H = 30 },
                new { Coordinate = new Coordinate("0,3"), G = 14, H = 60 },
                new { Coordinate = new Coordinate("1,3"), G = 10, H = 50 },
                new { Coordinate = new Coordinate("2,3"), G = 14, H = 40 }
            };

            Assert.Equal(expectedNodes.Length, reachableNodes.Count);

            foreach (var item in expectedNodes)
            {
                var node = reachableNodes.FirstOrDefault(q => q.MapNode.Coordinate.Equals(item.Coordinate));

                Assert.True(node != null);
                Assert.Equal(item.G, node.G);
                Assert.Equal(item.H, node.H);
                Assert.Equal(startNode, node.Parent);
            }
        }

        [Fact]
        public void GetReachableNodes_WorstPath()
        {
            var manager = new NodeManager(Map);
            var startNode = manager.GetOrCreateNode("1,2");
            var endNode = manager.GetOrCreateNode("5,2");
            Node nextNode;

            _ = startNode.GetReachableNodes(endNode).ToArray();
            nextNode = manager.GetOrCreateNode("2,2");
            _ = nextNode.GetReachableNodes(endNode).ToArray();

            var node = manager.GetOrCreateNode("2,1");
            Assert.Equal(14, node.G);
            Assert.Equal(40, node.H);
            Assert.Equal(new Coordinate("1,2"), node.Parent!.MapNode.Coordinate);
        }

        [Fact]
        public void GetReachableNodes_BetterPath()
        {
            var manager = new NodeManager(Map);
            var startNode = manager.GetOrCreateNode("1,2");
            var endNode = manager.GetOrCreateNode("5,2");
            Node nextNode;

            _ = startNode.GetReachableNodes(endNode).ToArray();
            nextNode = manager.GetOrCreateNode("2,2");
            _ = nextNode.GetReachableNodes(endNode).ToArray();
            nextNode = manager.GetOrCreateNode("2,3");
            _ = nextNode.GetReachableNodes(endNode).ToArray();

            var node = manager.GetOrCreateNode("1,4");
            Assert.Equal(28, node.G);
            Assert.Equal(60, node.H);
            Assert.Equal(new Coordinate("2,3"), node.Parent!.MapNode.Coordinate);

            nextNode = manager.GetOrCreateNode("1,3");
            _ = nextNode.GetReachableNodes(endNode).ToArray();

            node = manager.GetOrCreateNode("1,4");
            Assert.Equal(20, node.G);
            Assert.Equal(60, node.H);
            Assert.Equal(new Coordinate("1,3"), node.Parent!.MapNode.Coordinate);
        }
    }
}
