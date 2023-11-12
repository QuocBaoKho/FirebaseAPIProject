using Firebase.Database;
using FirebaseAPIProject.Models;
using FirebaseAPIProject.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirebaseAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public PostService postService;
        public PostsController()
        {
            postService = new PostService();
        }
        public PostsController(PostService postService)
        {
            this.postService = postService;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet("Lavisto")]

        public async Task<List<KeyValuePair<string, Post>>> GetUsers()
        {
            var posts = await postService.extractData();
            return posts;
        }
        [HttpPut("PutNewPost")]
        public async Task<ActionResult<Post>> UpdatePost(string id, Post post)
        {
            try
            {
                await postService.putNew(id, post);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            }
        [HttpPost("Post")]
        public async Task<ActionResult<Post>> PostNewPost(Post post)
        {
            try
            {
                if(post == null)
                {
                    return BadRequest();
                }
                var newPost = await postService.postNew(post);
                return newPost == null ? NotFound() : Ok(newPost);
            }
            catch
            {
                return BadRequest();
            }
            
           
        }
    }
}
