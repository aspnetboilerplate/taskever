using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Taskever.Security.Users;

namespace Taskever.Activities
{
    public class UserFollowedActivity : Entity<long>, IHasCreationTime
    {
        [ForeignKey("UserId")]
        public virtual TaskeverUser User { get; set; }

        public virtual long UserId { get; set; }

        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }

        public virtual long ActivityId { get; set; }

        public virtual bool IsActor { get; set; }

        public virtual bool IsRelated { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public UserFollowedActivity()
        {
            CreationTime = DateTime.Now;
        }
    }
}
