using Abp.Authorization.Roles;
using Taskever.Security.Users;

namespace Taskever.Security.Roles
{
    public class TaskeverRole : AbpRole<TaskeverUser>
    {
        //no additional field yet
    }
}