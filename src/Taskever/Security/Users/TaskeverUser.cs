using Abp.Authorization.Users;

namespace Taskever.Security.Users
{
    public class TaskeverUser : AbpUser<TaskeverUser>
    {
        /// <summary>
        /// Profile image of the user. 
        /// </summary>
        public virtual string ProfileImage { get; set; }
    }
}
