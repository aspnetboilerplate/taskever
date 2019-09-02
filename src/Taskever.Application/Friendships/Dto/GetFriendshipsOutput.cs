using System.Collections.Generic;

namespace Taskever.Friendships.Dto
{
    public class GetFriendshipsOutput 
    {
        public IList<FriendshipDto> Friendships { get; set; }
    }
}