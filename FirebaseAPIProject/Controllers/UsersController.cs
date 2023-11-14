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
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
                    return newUser is null?BadRequest():Ok(newUser);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
            
         }
        [HttpPut("PutNewUsers")]
        public async Task<ActionResult<Post>> UpdateUser(string id, User user)
        {
            try
            {
                await userService.putNew(id, user);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
