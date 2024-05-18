using Onlineauction.Models;
using Server.Dtos.User;

namespace Server.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role
            };

        }



        public static User ToUserFromCreate(this CreateUserDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Name = userDto.Name
            };
        }


    }
}
