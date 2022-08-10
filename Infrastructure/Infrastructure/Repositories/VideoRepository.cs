using Application.Contracts.Persistence;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(VideoDbContext context) : base(context)
        { }

        public async Task<Video> GetVideoByNombre(string nombreVideo)
        {
            return await _context.Videos.Where(p => p.Name == nombreVideo).FirstOrDefaultAsync();
        }
    }
}