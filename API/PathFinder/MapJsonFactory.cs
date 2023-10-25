using Microsoft.Extensions.Caching.Memory;

namespace API.PathFinder
{
    public class MapJsonFactory
    {
        private const string JsonCacheKey = "jsonMap";
        private readonly TimeSpan CacheTime = TimeSpan.FromMinutes(30);
        private readonly IMemoryCache Cache;

        public MapJsonFactory(IMemoryCache cache)
        {
            Cache = cache;
        }

        public async Task<string> GetJsonAsync()
        {
            string? json = await GetChessboardMapJsonCacheAsync();
            if (json == null) throw new ApplicationException($"Could not load chessboard map");

            return json;
        }

        private async Task<string?> GetChessboardMapJsonCacheAsync() => await Cache.GetOrCreateAsync(JsonCacheKey,
                    async cacheEntry =>
                    {
                        cacheEntry.SlidingExpiration = CacheTime;
                        return await GetChessboardMapJsonAsync();
                    });

        private static async Task<string> GetChessboardMapJsonAsync()
        {
            using HttpClient client = new();
            var response = await client.GetAsync("https://mocki.io/v1/10404696-fd43-4481-a7ed-f9369073252f");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
