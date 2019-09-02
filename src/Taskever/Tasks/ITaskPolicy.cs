using Abp.Domain.Policies;
using Taskever.Security.Users;

namespace Taskever.Tasks
{
    //TODO: Renamt this to Policy ?
    public interface ITaskPolicy : IPolicy
    {
        bool CanSeeTasksOfUser(TaskeverUser requesterUser, TaskeverUser userOfTasks);

        bool CanAssignTask(TaskeverUser assignerUser, TaskeverUser userToAssign);

        bool CanUpdateTask(TaskeverUser user, Task task);

        bool CanDeleteTask(TaskeverUser user, Task task);
    }
}