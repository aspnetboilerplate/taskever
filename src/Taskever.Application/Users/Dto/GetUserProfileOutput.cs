using Abp.Users.Dto;

namespace Taskever.Users.Dto
{
    public class GetUserProfileOutput 
    {
        public bool CanNotSeeTheProfile { get; set; }

        public bool SentFriendshipRequest { get; set; }

        public UserDto User { get; set; }
    }
}