using Abp.Domain.Policies;
using Taskever.Security.Users;

namespace Taskever.Friendships
{
    public interface IFriendshipPolicy : IPolicy
    {
        bool CanChangeFriendshipProperties(TaskeverUser currentUser, Friendship friendShip);
        bool CanRemoveFriendship(TaskeverUser currentUser, Friendship friendship);
    }
}