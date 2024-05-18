using Microsoft.AspNetCore.Mvc;
using Onlineauction.Interfaces;
using Onlineauction.Models;
using Server.Dtos.User;
using Server.Mappers;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {

            var userModel = userDto.ToUserFromCreate();
            int id = await _userRepository.AddUserReturnIdAsync(userModel);
            userModel.Id = id;

            return CreatedAtAction(nameof(GetUserById), new { id = userModel.Id}, userModel.ToUserDto());
        }



        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users.Count == 0 || users == null )
            {
                return NoContent();
            }
            return Ok(users);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) 
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            var updatedUser = await _userRepository.UpdateUserAsync(id, userDto);

            if(updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser); // change
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            if (!await _userRepository.UserExists(id)) 
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }
   
    }
}
