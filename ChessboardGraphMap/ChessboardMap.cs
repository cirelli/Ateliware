using System.Text.Json;
using A_Star;

namespace ChessboardGraphMap
{
    public class ChessboardMap : IMap
    {
        internal readonly Dictionary<Coordinate, Location> Locations = new();
        internal readonly int AverageTimeToCross;

        public ChessboardMap(Stream stream)
            : this(JsonSerializer.Deserialize<JsonDocument>(stream))
        { }

        public ChessboardMap(string json)
            : this(JsonSerializer.Deserialize<JsonDocument>(json))
        { }

        private ChessboardMap(JsonDocument? jsonDocument)
        {
            if (jsonDocument == null) throw new ArgumentNullException(nameof(jsonDocument));

            AverageTimeToCross = LoadLocations(jsonDocument);
        }

        private int LoadLocations(JsonDocument jsonDocument)
        {
            int sumOfTime = 0;
            int numberOfRoads = 0;

            var locations = jsonDocument!.RootElement.EnumerateObject().ToArray();
            foreach (var property in locations)
            {
                var location = GetOrCreateLocation(property.Name);
                var roads = property.Value.EnumerateObject().ToArray();
                foreach (var road in roads)
                {
                    var roadLocation = GetOrCreateLocation(road.Name);
                    var time = road.Value.GetDecimal();

                    var r = new Road(roadLocation, time);
                    location.Roads.Add(r);

                    numberOfRoads++;
                    sumOfTime += r.TimeToCross;
                }
            }

            return sumOfTime / numberOfRoads;
        }

        private Location? TryGetLocation(string coordinate)
        {
            var coord = new Coordinate(coordinate);
            Locations.TryGetValue(coord, out var loc);

            return loc;
        }

        private Location GetOrCreateLocation(string coordinate)
        {
            var loc = TryGetLocation(coordinate);

            if (loc == null)
            {
                loc = new Location(coordinate, this);
                Locations.Add(loc.Coordinate, loc);
            }

            return loc;
        }

        public IMapNode GetNode(string coordinate)
        {
            var loc = TryGetLocation(coordinate);

            if (loc == null) throw new ArgumentOutOfRangeException(nameof(coordinate), "Coordinate does not exist!");

            return loc;
        }

        public IEnumerable<IReachableMapNode> GetReachableNodes(IMapNode start, IMapNode destination)
        {
            if (start is not Location locationStart) throw new ArgumentException("Invalid parameter.", nameof(start));
            if (destination is not Location location) throw new ArgumentException("Invalid parameter.", nameof(destination));

            foreach (var road in locationStart.Roads)
            {
                yield return new ReachableMapNode(road.To)
                {
                    CostToMove = road.TimeToCross,
                    CostToMoveToDestination = road.To.Coordinate.DistanceTo(location.Coordinate) * AverageTimeToCross
                };
            }
        }
    }
}
