
using System;

namespace Taskever.Activities
{
    public class CompleteTaskActivity : Activity
    {
        public CompleteTaskActivity()
        {
            //ActivityType = ActivityType.CompleteTask;            
        }

        public override long?[] GetActors()
        {
            return new[] { (long?)AssignedUserId };
        }

        public override long?[] GetRelatedUsers()
        {
            if (Task == null)
            {
                throw new ArgumentNullException("Task can not be null. Include Task to call GetRelatedUsers method");
            }
            return new[] { Task.CreatorUserId };
        }
    }
}