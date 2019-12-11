using AutoMapper;
using Taskever.Security.Users;

namespace Abp.Users.Dto
{
    public class UserDtosMapper : Profile
    {
        public UserDtosMapper()
        {
            CreateMap<TaskeverUser, UserDto>()
                .ForMember(
                    user => user.ProfileImage,
                    configuration => configuration.MapFrom(
                        user => user.ProfileImage == null
                                    //TODO: How to implement this?
                                    ? ""
                                    : "ProfileImages/" + user.ProfileImage
                                         )
                ).ReverseMap();

            CreateMap<RegisterUserInput, TaskeverUser>();

            CreateMap<TaskeverUser, UserDto>().ReverseMap();
        }
    }
}
