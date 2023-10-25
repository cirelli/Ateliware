using System.ComponentModel.DataAnnotations;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathFinderController : ControllerBase
    {
        private static readonly List<DeliveryViewModel> LatestDeliveries = new();

        private readonly PathFinder.PathFinder PathFinder;

        public PathFinderController(PathFinder.PathFinder pathFinder)
        {
            PathFinder = pathFinder;
        }

        [HttpGet]
        public async Task<ActionResult<PathFinderViewModel>> FastestPath([Required] string startingPosition, [Required] string objectPosition, [Required] string destination)
        {
            var pathFound = await PathFinder.FindPath(startingPosition, objectPosition, destination);

            var result = new PathFinderViewModel
            {
                Path = pathFound.Path,
                TotalTime = pathFound.TotalTime,
                LatestDeliveries = new List<DeliveryViewModel>(LatestDeliveries)
            };

            AddLatestDelivery(startingPosition, objectPosition, destination, pathFound.TotalTime);

            return result;
        }

        private static void AddLatestDelivery(string start, string pickup, string destination, float totalTime)
            => LatestDeliveries.Insert(0, new DeliveryViewModel { Start = start, Pickup = pickup, Destination = destination, TotalTime = totalTime });
    }
}
