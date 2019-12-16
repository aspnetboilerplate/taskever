using Abp.Users.Dto;
using AutoMapper;
using Taskever.Security.Users;

namespace Taskever.Mapping
{
    public class UserDtosMapper
    {
        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TaskeverUser, UserDto>()
                .ForMember(
                    user => user.ProfileImage,
                    configuration => configuration.MapFrom(
                        user => user.ProfileImage == null
                                    //TODO: How to implement this?
                                    ? ""
                                    : "ProfileImages/" + user.ProfileImage
                                         )
                ).ReverseMap();

            cfg.CreateMap<RegisterUserInput, TaskeverUser>();
            cfg.CreateMap<TaskeverUser, UserDto>().ReverseMap();
        }
    }
}
