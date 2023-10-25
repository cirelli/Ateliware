using A_Star;
using API.Models;
using ChessboardGraphMap;

namespace API.PathFinder
{
    public class PathFinder
    {
        private readonly MapJsonFactory MapJsonFactory;
        private readonly Lazy<Task<IMap>> lazyMap;
        private readonly Lazy<Task<AStar>> lazyAStar;

        public PathFinder(MapJsonFactory mapJsonFactory)
        {
            MapJsonFactory = mapJsonFactory;

            lazyMap = new Lazy<Task<IMap>>(async () => new ChessboardMap(await MapJsonFactory.GetJsonAsync()));
            lazyAStar = new Lazy<Task<AStar>>(async () => new AStar(await lazyMap.Value));
        }

        public async Task<PathFinderResult> FindPath(string startPosition, string pickupPosition, string destinationPosition)
        {
            var aStar = await lazyAStar.Value;

            var pickupRoute = aStar.FindPath(startPosition, pickupPosition);
            var deliveryRoute = aStar.FindPath(pickupPosition, destinationPosition);

            return new PathFinderResult
            {
                Path = $"{pickupRoute.Path}+{deliveryRoute.Path}",
                TotalTime = (pickupRoute.TotalCost + deliveryRoute.TotalCost) / 1000f
            };
        }
    }
}
