using Abp.Domain.Policies;

namespace Taskever.Security.Users
{
    public interface ITaskeverUserPolicy : IPolicy
    {
        bool CanSeeProfile(TaskeverUser requesterUser, TaskeverUser targetUser);
    }
}
