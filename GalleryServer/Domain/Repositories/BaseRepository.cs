using Domain.Context;
using Domain.Interfaces;

namespace Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }
        protected RepositoryContextFactory ContextFactory { get; }
        public BaseRepository(string connectionString, IRepositoryContextFactory contextFactory)
        {
            ConnectionString = connectionString;
            ContextFactory = contextFactory as RepositoryContextFactory;
        }
    }
}
