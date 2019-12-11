using AutoMapper;
using Taskever.Activities;
using Taskever.Activities.Dto;
using Taskever.Friendships;
using Taskever.Friendships.Dto;
using Taskever.Tasks;
using Taskever.Tasks.Dto;

namespace Taskever.Mapping
{
    public class TaskeverDtoMapper : Profile
    {
        public TaskeverDtoMapper()
        {
            //TODO: Check unnecessary ReverseMaps
            CreateMap<Task, TaskDto>().ReverseMap();
            CreateMap<Task, TaskWithAssignedUserDto>().ReverseMap();
            CreateMap<Friendship, FriendshipDto>().ReverseMap();

            CreateMap<Activity, ActivityDto>()
                  .Include<CreateTaskActivity, CreateTaskActivityDto>()
                  .Include<CompleteTaskActivity, CompleteTaskActivityDto>();
            CreateMap<CreateTaskActivity, CreateTaskActivityDto>().ForMember(t => t.ActivityType, tt => tt.MapFrom(x => 1));
            CreateMap<CompleteTaskActivity, CompleteTaskActivityDto>().ForMember(t => t.ActivityType, tt => tt.MapFrom(x => 2));
            CreateMap<UserFollowedActivity, UserFollowedActivityDto>();
        }
    }
}
