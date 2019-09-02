using System.Collections.Generic;

namespace Taskever.Activities.Dto
{
    public class GetUserActivitiesOutput 
    {
        public IList<UserFollowedActivityDto> Activities { get; set; }
    }
}