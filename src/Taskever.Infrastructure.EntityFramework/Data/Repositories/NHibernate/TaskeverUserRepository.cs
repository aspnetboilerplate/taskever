using Abp.EntityFramework;
using Taskever.Security.Users;

namespace Taskever.Infrastructure.EntityFramework.Data.Repositories.NHibernate
{
    public class TaskeverUserRepository : TaskeverEfRepositoryBase<TaskeverUser, long>, ITaskeverUserRepository
    {
        public TaskeverUserRepository(IDbContextProvider<TaskeverDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}