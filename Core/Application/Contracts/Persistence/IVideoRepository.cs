using Domain;
using Domain.Common;

namespace Application.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
         Task<Video> GetVideoByNombre(string nombreVideo);
    }
}