using System.Collections;
using Application.Contracts.Persistence;
using Domain.Common;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly VideoDbContext _context;

        //Aca van los repositorios
        private IVideoRepository _videoRepository;

        //Inyeccion por propiedades
        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context);

        public UnitOfWork(VideoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.Contains(type))
            {
                var repositoryType = typeof(IAsyncRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)));
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}