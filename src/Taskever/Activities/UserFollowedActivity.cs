using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Taskever.Security.Users;

namespace Taskever.Activities
{
    public class UserFollowedActivity : Entity<long>, IHasCreationTime
    {
        public virtual TaskeverUser User { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual bool IsActor { get; set; }

        public virtual bool IsRelated { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public UserFollowedActivity()
        {
            CreationTime = DateTime.Now;
        }
    }
}
