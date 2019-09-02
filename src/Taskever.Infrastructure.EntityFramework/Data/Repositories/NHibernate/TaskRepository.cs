using Abp.EntityFramework;
using Taskever.Tasks;

namespace Taskever.Infrastructure.EntityFramework.Data.Repositories.NHibernate
{
    public class TaskRepository : TaskeverEfRepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(IDbContextProvider<TaskeverDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
