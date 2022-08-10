using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class VideoDbContextSeed
    {
        public static async Task SeedAsync(VideoDbContext context, ILogger<VideoDbContextSeed> logger)
        {
            if(!context.Videos.Any())
            {
                context.Videos.AddRange(GetPreconfiguratedVideo());
                await context.SaveChangesAsync();
                logger.LogInformation("Insertando nuevos registros");
            }
        }

        private static IEnumerable<Video> GetPreconfiguratedVideo()
        {
            return new List<Video>
            {
                new Video 
                {
                    Id = 0,
                    Name = "Valor Seed"
                }
            };
        }
    }
}