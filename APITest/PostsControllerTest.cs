using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAPIProject.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using FirebaseAPIProject.Services;
using FirebaseAPIProject.Models;
using Firebase.Database;

using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Hosting;
using Firebase.Database.Query;

namespace APITest
{
    [TestClass]

    public class PostsControllerTest
    {
        string testLink = "https://friendversetest-default-rtdb.asia-southeast1.firebasedatabase.app/";
        PostService postService;
        
       //Task<IReadOnlyCollection<FirebaseObject<Post>>> mockPosts;
        public PostsControllerTest() {
            FirebaseClient firebaseClient = new FirebaseClient(testLink);
            postService = new PostService(firebaseClient);
            
            
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
            List<KeyValuePair<string, Post>> expectedResult = await postService.extractData();
            List<Post> expectedValues = (from post in expectedResult select post.Value).ToList();
            PostsController controller = new PostsController();
            controller.postService = postService;

            List<Post> result = await controller.GetPosts();

            CollectionAssert.AreEqual(expectedValues, result);


        }
       
        [TestMethod]
        public async Task PostMethod()
        {
            var newPost = new Post(1412553, "Yappy", "image", "Mahiyo", "data:image/jpeg;base64,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", "", "Miegwpwe");  
            PostsController controller = new PostsController();
            controller.postService = postService;
            var result = await controller.PostNewPost(newPost);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
        [TestMethod]

        public async Task PostMethod2()
        {
            var newPost = new Post(1412553, "Michael Jackson is cool", "image2", "-Nj0kkh9mQoXh1vUWTpS", "default", "", "Miegwpwe");

            PostsController controller = new PostsController();
            controller.postService = postService;
            var result = await controller.PostNewPost(newPost);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);

        }

       
        [TestMethod]
        public async Task PutMethod()
        {
            var newPost = new Post(1412553, "Yeehawaa", "image", "-Nj0kkh9mQoXh1vUWTpS", "data:image/jpeg;base64,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", "", "Miegwpwe");

            PostsController controller = new PostsController();
            controller.postService = postService;

            var result = await controller.UpdatePost("-Nj0kkh9mQoXh1vUWTpS", newPost);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);

        }
        [TestMethod]
        public async Task PutMethod2()
        {
            var newPost = new Post(1412553, "Yeehawaa", "image", "-Nj0kkh9mQoXh1vUWTpS", " ", "", "Miegwpwe");

            PostsController controller = new PostsController();
            controller.postService = postService;

            var result = await controller.UpdatePost("-Nj0kkh9mQoXh1vUWTpS", newPost);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);

        }
        [TestMethod]
        public async Task DeleteMethod()
        {
            Post x = new Post(2435353, "", "image", "idsan", "https://www.alleycat.org/wp-content/uploads/2019/03/FELV-cat.jpg", "", "YWzMcYcArDhHnnFB5JQx9MPy8ep1");
            FirebaseClient client = new FirebaseClient(testLink);

            PostsController controller = new PostsController();
            controller.postService = postService;
            await client.Child("Posts").PostAsync(x);
            var data = await postService.extractData();
            var key = (from post1 in data where post1.Value.Postid == x.Postid select post1.Key).FirstOrDefault();
            string postid = key;
            var result = await controller.DeletePost(postid);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
        
    }
}
