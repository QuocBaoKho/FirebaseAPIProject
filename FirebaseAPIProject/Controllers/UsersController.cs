using Microsoft.AspNetCore.Mvc;
using FirebaseAPIProject.Models;
using FirebaseAPIProject.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirebaseAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UserService userService;
        public UsersController()
        {
            userService = new UserService();
        }
        
        [HttpGet("Lavisto")]
        public async Task<List<User>> GetUsers() { 
            var users = await userService.extractData();
            var realUsers = (from user in users select user.Value).ToList();
            return realUsers;
        }
        [HttpGet("viaID")]        
        public async Task<User> GetUserViaID(string ID)
        {
            var users = await userService.extractData();
            var IDuser = (from user in users where user.Key == ID select user.Value ).FirstOrDefault();
            return IDuser;
        }
        [HttpPost("NewUser")]
        public async Task<ActionResult<User>> addNewUser(User user)
        {
            try
            {
                if (user != null)
                {
                    var newUser = await userService.addUser(user);
                    return newUser is null? NotFound() : Ok(newUser);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
            
         }
        [HttpPut("PutNewUsers")]
        public async Task<ActionResult<User>> UpdateUser(string id, User user)
        {
            try
            {
                string result = await userService.putNew(id, user);
                return result == null? NotFound() : Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            string y = await userService.deleteUser(id);
            return y == null ? Ok(y) : BadRequest();
        }
    }
}
