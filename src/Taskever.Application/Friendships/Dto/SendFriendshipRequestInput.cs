using System.ComponentModel.DataAnnotations;

namespace Taskever.Friendships.Dto
{
    public class SendFriendshipRequestInput 
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}