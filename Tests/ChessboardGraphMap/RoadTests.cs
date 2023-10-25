using ChessboardGraphMap;

namespace Tests.ChessboardGraphMap
{
    public class RoadTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(1.2)]
        [InlineData(1.35)]
        [InlineData(1.795)]
        public void TimeToCross_InMiliseconds(decimal timeToCross)
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var obj = new Road(null, timeToCross);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.Equal(timeToCross * 1000, obj.TimeToCross);
        }
    }
}
