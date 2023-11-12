using FirebaseAPIProject.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace APITest
{
    [TestClass]
    public class UnitTest1
    {
        private static int? GetStatusCode<T>(ActionResult<T?> actionResult)
        {
            IConvertToActionResult convertToActionResult = actionResult; // ActionResult implicit implements IConvertToActionResult
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }
        [TestMethod]
        public void UserRetrievalTest()
        {
            UsersController usersController = new UsersController();
            Assert.IsNotNull(usersController.GetUsers(), "It's not null");

        }
        [TestMethod]
        public void PostRetrievalTest() {
            PostsController postsController = new PostsController();
            Assert.IsNotNull(postsController.GetUsers(), "YAH YAH");
        
        }
    }
}
