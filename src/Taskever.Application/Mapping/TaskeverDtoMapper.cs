using AutoMapper;
using Taskever.Activities;
using Taskever.Activities.Dto;
using Taskever.Friendships;
using Taskever.Friendships.Dto;
using Taskever.Tasks;
using Taskever.Tasks.Dto;

namespace Taskever.Mapping
{
    public class TaskeverDtoMapper
    {
        public void Map(IMapperConfigurationExpression cfg)
        {
            //TODO: Check unnecessary ReverseMaps
            cfg.CreateMap<Task, TaskDto>().ReverseMap();
            cfg.CreateMap<Task, TaskWithAssignedUserDto>().ReverseMap();
            cfg.CreateMap<Friendship, FriendshipDto>().ReverseMap();

            cfg.CreateMap<Activity, ActivityDto>()
                  .Include<CreateTaskActivity, CreateTaskActivityDto>()
                  .Include<CompleteTaskActivity, CompleteTaskActivityDto>();
            cfg.CreateMap<CreateTaskActivity, CreateTaskActivityDto>().ForMember(t => t.ActivityType, tt => tt.MapFrom(x => 1));
            cfg.CreateMap<CompleteTaskActivity, CompleteTaskActivityDto>().ForMember(t => t.ActivityType, tt => tt.MapFrom(x => 2));
            cfg.CreateMap<UserFollowedActivity, UserFollowedActivityDto>();
        }
    }
}
