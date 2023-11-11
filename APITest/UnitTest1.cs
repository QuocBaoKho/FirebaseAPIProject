using FirebaseAPIProject.Controllers;
namespace APITest
{
    [TestClass]
    public class UnitTest1
    {
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
