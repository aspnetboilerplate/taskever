using Abp.Domain.Services;
using Taskever.Security.Users;

namespace Taskever.Friendships
{
    public interface IFriendshipDomainService : IDomainService
    {
        bool HasFriendship(TaskeverUser user, TaskeverUser probableFriend);
    }
}