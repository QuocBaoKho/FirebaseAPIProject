using FirebaseAPIProject.Models;
using FirebaseAPIProject.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirebaseAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public CommentService commentService;
        public CommentsController()
        {
            this.commentService = new CommentService();
        }
        [HttpGet]
        public async Task<List<Comments>> GetComments(string postID)
        {
            var comments = await commentService.getPost_sComments(postID);
            return (from comment in comments select comment.Value).ToList();
        }
        [HttpPost]
        public async Task<ActionResult<Comments>> AddComment(string postID, Comments _comment)
        {
            try
            {
                if (_comment is null) return BadRequest();
                var newComment = await commentService.PostComment(postID, _comment);
                return newComment is null? NotFound() : Ok(newComment);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete] 
        
        public async Task<ActionResult<string>> DeleteComment(string postID, string commentID)
        {
            var key = await commentService.DeleteComment(postID, commentID);
            return key == ""? Ok(key): BadRequest();
        }
    }
}
