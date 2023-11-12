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
        UserService userService;
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
        public async Task<List<KeyValuePair<string, User>>> GetUsers() { 
            var users = await userService.extractData();
            return users;
        }
        [HttpPost("NewUser")]
        public async Task addNewUser(User user)
        {
            await userService.addUser(user);
        }
    }
}
