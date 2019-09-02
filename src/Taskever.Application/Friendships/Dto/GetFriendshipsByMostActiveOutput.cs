using System.Collections.Generic;

namespace Taskever.Friendships.Dto
{
    public class GetFriendshipsByMostActiveOutput 
    {
        public IList<FriendshipDto> Friendships { get; set; }
    }
}