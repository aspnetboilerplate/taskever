using System.ComponentModel.DataAnnotations.Schema;
using Taskever.Security.Users;

namespace Taskever.Activities
{
    public class CreateTaskActivity : Activity
    {
        [ForeignKey("CreatorUserId")]
        public virtual TaskeverUser CreatorUser { get; set; }

        public virtual long CreatorUserId { get; set; }

        public CreateTaskActivity()
        {
            //ActivityType = ActivityType.CreateTask;
        }

        public override long?[] GetActors()
        {
            return new long?[] { CreatorUserId, AssignedUserId };
        }

        public override long?[] GetRelatedUsers()
        {
            return new long?[] { };
        }
    }
}