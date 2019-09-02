namespace Abp.Users.Dto
{
    public class GetUserOutput 
    {
        public UserDto User { get; set; }

        public GetUserOutput(UserDto user)
        {
            User = user;
        }
    }
}