using GridMap;

namespace Tests.AStar
{
    public class AStarTests
    {
        private readonly Map Map = new(new bool[,] {
                {true, true, true, true, true, true, true},
                { true, true, true, false, true, true, true},
                {true, true, true, false, true, true, true},
                {true, true, true, false, true, true, true},
                {true, true, true, true, true, true, true}
            });

        [Theory]
        [InlineData("1,2", "5,2", "(1,2)-(2,1)+(2,1)-(3,0)+(3,0)-(4,1)+(4,1)-(5,2)", 56)]
        [InlineData("0,0", "6,4", "(0,0)-(1,1)+(1,1)-(2,2)+(2,2)-(2,3)+(2,3)-(3,4)+(3,4)-(4,4)+(4,4)-(5,4)+(5,4)-(6,4)", 82)]
        public void FindPath(string coordStart, string coordEnd, string expectedPath, int expectedCost)
        {
            var aStar = new A_Star.AStar(Map);
            var path = aStar.FindPath(coordStart, coordEnd);

            Assert.Equal(expectedPath, path.Path);
            Assert.Equal(expectedCost, path.TotalCost);
        }
    }
}
