using Abp.EntityFramework;
using Taskever.Activities;

namespace Taskever.Infrastructure.EntityFramework.Data.Repositories.NHibernate
{
    public class ActivityRepository : TaskeverEfRepositoryBase<Activity, long>
    {
        public ActivityRepository(IDbContextProvider<TaskeverDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}