using Core.Domain.Common;

namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();

        IVideoRepository VideoRepository { get; }
    }
}