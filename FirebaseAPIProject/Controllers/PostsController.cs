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
        //public PostsController(PostService postService)
        //{
           
          //  this.postService = postService;
        //}

        [HttpGet("Lavisto")]

        public async Task<List<Post>> GetPosts()
        {
            var posts = await postService.extractData();
            List<Post> actualValues = (from post in posts select post.Value).ToList();
            return actualValues;
        }
        [HttpGet("viaID")]
        public async Task<Post> GetPostViaID(string ID)
        {
            try
            {
                var posts = await postService.extractData();
                Post actualValues = (from post in posts where post.Key == ID select post.Value).FirstOrDefault();
                return actualValues;
            }catch(Exception e)
            {
                throw new Exception();
            }
            
        }
        [HttpPut("PutNewPost")]
        public async Task<ActionResult<Post>> UpdatePost(string id, Post post)
        {
            try
            {
                var result = await postService.putNew(id, post);
                return result == null? NotFound(): Ok();
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
        [HttpDelete]
        public async Task<ActionResult<string>> DeletePost(string id)
        {
            string key = await postService.DeletePost(id);
            return key == null ? Ok(key) : BadRequest();
        }
    }
}
