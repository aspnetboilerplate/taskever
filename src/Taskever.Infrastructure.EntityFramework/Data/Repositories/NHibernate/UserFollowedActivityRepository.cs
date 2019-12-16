using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.EntityFramework;
using Taskever.Activities;

namespace Taskever.Infrastructure.EntityFramework.Data.Repositories.NHibernate
{
    public class UserFollowedActivityRepository : TaskeverEfRepositoryBase<UserFollowedActivity, long>, IUserFollowedActivityRepository
    {
        public UserFollowedActivityRepository(IDbContextProvider<TaskeverDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public IList<UserFollowedActivity> Getactivities(long userId, bool? isActor, long? beforeId, int maxResultCount)
        {
            var query = GetAll().Include(x => x.User).Include(x => x.Activity).Where(a => a.User.Id == userId);

            if (isActor.HasValue)
            {
                query = query.Where(x => x.IsActor == isActor);
            }

            if (beforeId.HasValue && beforeId != 0)
            {
                query = query.Where(q => q.Id < beforeId);
            }

            return query.Take(maxResultCount).ToList();
        }
    }
}