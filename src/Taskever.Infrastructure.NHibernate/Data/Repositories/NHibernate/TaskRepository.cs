using Abp.NHibernate;
using Abp.NHibernate.Repositories;
using Taskever.Tasks;

namespace Taskever.Data.Repositories.NHibernate
{
    public class TaskRepository : NhRepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(ISessionProvider sessionProvider) : base(sessionProvider)
        {
        }
    }
}
