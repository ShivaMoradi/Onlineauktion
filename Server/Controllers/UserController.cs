using Microsoft.AspNetCore.Mvc;
using Onlineauction.Interfaces;
using Onlineauction.Models;

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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUserAsync(user);
            var updatedUser = GetUserById(id);
            return Ok(updatedUser);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user == null) 
            {
                return BadRequest("User not found");
            }

            _userRepository.DeleteUserAsync(id);
            return NoContent();
        }
   
    }
}
