using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Taskever.Infrastructure.EntityFramework.Data.Repositories.NHibernate
{
    public abstract class TaskeverEfRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TaskeverDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TaskeverEfRepositoryBase(IDbContextProvider<TaskeverDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public abstract class TaskeverEfRepositoryBase<TEntity> : EfRepositoryBase<TaskeverDbContext,TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TaskeverEfRepositoryBase(IDbContextProvider<TaskeverDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}