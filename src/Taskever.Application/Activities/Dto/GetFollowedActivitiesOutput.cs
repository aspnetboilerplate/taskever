using System.Collections.Generic;

namespace Taskever.Activities.Dto
{
    public class GetFollowedActivitiesOutput 
    {
        public IList<UserFollowedActivityDto> Activities { get; set; }
    }
}