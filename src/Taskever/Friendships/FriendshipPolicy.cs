using Taskever.Security.Users;

namespace Taskever.Friendships
{
    public class FriendshipPolicy : IFriendshipPolicy
    {
        public bool CanChangeFriendshipProperties(TaskeverUser user, Friendship friendShip)
        {
            //Can change only his own friendships.
            return user.Id == friendShip.User.Id;
        }

        public bool CanRemoveFriendship(TaskeverUser currentUser, Friendship friendship)
        {
            return friendship.User.Id == currentUser.Id;
        }
    }
}