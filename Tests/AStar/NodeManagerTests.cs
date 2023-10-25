using A_Star;
using GridMap;

namespace Tests.AStar
{
    public class NodeManagerTests
    {
        [Fact]
        public void GetOrCreateNode()
        {
            var manager = new NodeManager(new Map(new bool[,] { { true, true } }));
            var node1 = manager.GetOrCreateNode("0,0");
            var node2 = manager.GetOrCreateNode("0,0");
            var node3 = manager.GetOrCreateNode("1,0");

            Assert.True(node1 == node2);
            Assert.True(node1 != node3);
        }
    }
}
