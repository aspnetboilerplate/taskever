using Abp.NHibernate;
using Abp.NHibernate.Repositories;
using Taskever.Security.Users;

namespace Taskever.Data.Repositories.NHibernate
{
    public class TaskeverUserRepository : NhRepositoryBase<TaskeverUser, long>, ITaskeverUserRepository
    {
        public TaskeverUserRepository(ISessionProvider sessionProvider) : base(sessionProvider)
        {
        }
    }
}