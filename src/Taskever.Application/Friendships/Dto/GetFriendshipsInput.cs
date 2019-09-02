using System.ComponentModel.DataAnnotations;

namespace Taskever.Friendships.Dto
{
    public class GetFriendshipsInput 
    {
        [Range(1, int.MaxValue)]
        public long UserId { get; set; }

        public FriendshipStatus? Status { get; set; }

        public bool? CanAssignTask { get; set; }
    }
}