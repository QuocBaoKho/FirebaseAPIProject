using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using FirebaseAPIProject.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using FirebaseAPIProject.Models;
using FirebaseAPIProject.Controllers;
using Microsoft.AspNetCore.Http;
using Firebase.Database.Query;

namespace APITest
{
    [TestClass]
    public class CommentsControllerTest
    {
        string testLink = "https://friendversetest-default-rtdb.asia-southeast1.firebasedatabase.app/";
        CommentService commentService;
        public CommentsControllerTest()
        {
            FirebaseClient client = new FirebaseClient(testLink);
            commentService = new CommentService(client);
        }
        private static int? GetStatusCode<T>(ActionResult<T?> actionResult)
        {
            IConvertToActionResult convertToActionResult = actionResult; // ActionResult implicit implements IConvertToActionResult
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }
        [TestMethod]
        public async Task GetMethod()
        {
            string postID = "-NVeSq29BLnq5kz2LwRh";
            List<KeyValuePair<string, Comments>> expectedResult = await commentService.getPost_sComments(postID);
            List<Comments> expectedValues = (from comment in expectedResult select comment.Value).ToList();
            CommentsController controller = new CommentsController();
            controller.commentService = commentService;
            List<Comments> actualValues = await controller.GetComments(postID);
            CollectionAssert.AreEqual(expectedValues, actualValues);
            
        }
        [TestMethod]
        public async Task PostMethod()
        {
            string postID = "-NVeSq29BLnq5kz2LwRh";
            CommentsController controller = new CommentsController();
            controller.commentService = commentService;
            var newComment = new Comments("exitant", "You're a nerd", "YWzMcYcArDhHnnFB5JQx9MPy8ep1", postID);
            var result = await controller.AddComment(postID, newComment);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
        [TestMethod]
        public async Task PostMethod2()
        {
            string postID = "-NVjjKGOBN8fW6wx-c4l";
            CommentsController controller = new CommentsController();
            controller.commentService = commentService;
            var newComment = new Comments("exitant", "", "YWzMcYcArDhHnnFB5JQx9MPy8ep1", postID);
            var result = await controller.AddComment(postID, newComment);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
        [TestMethod]
        public async Task DeleteMethod()
        {
            string postID = "-NVeSq29BLnq5kz2LwRh";
            Comments comment = new Comments("nana", "Latto", "YWzMcYcArDhHnnFB5JQx9MPy8ep1", postID);
            FirebaseClient client = new FirebaseClient(testLink);

            CommentsController controller = new CommentsController();
            controller.commentService = commentService;
            await client.Child("Comments").Child(postID).PostAsync(comment);
            var data = await commentService.getPost_sComments(postID);
            var key = (from ex in data where ex.Value.CommentId == comment.CommentId select ex.Key).FirstOrDefault();
            var result = await controller.DeleteComment(postID, key);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
        
    }
}
