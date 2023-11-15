using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAPIProject.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using FirebaseAPIProject.Services;
using Firebase.Database;
using FirebaseAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Firebase.Database.Query;

namespace APITest
{
    [TestClass]

    public class UsersControllerTest
    {
        string testLink = "https://friendversetest-default-rtdb.asia-southeast1.firebasedatabase.app/";
        UserService userService;
        public UsersControllerTest()
        {
            FirebaseClient client = new FirebaseClient(testLink);
            userService = new UserService(client);
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
            List<KeyValuePair<string, User>> extract = await userService.extractData();
            List<User> expected = (from user in extract select user.Value).ToList();
            UsersController controller = new UsersController();
            controller.userService = userService;
            List<User> actual = await controller.GetUsers();
            CollectionAssert.AreEqual(expected, actual);

        }
        [TestMethod]
        public async Task PostMethod()
        {
            var user = new User(0, "I'm an ass man", "x@gmail.com", "Phu Van M", "-ewgeghrwhweAAA",
           "https://images.unsplash.com/photo-1608848461950-0fe51dfc41cb?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8fA%3D%3D",
           "2222222222", "FkBoy", "", "");
            UsersController controller = new UsersController();
            controller.userService = userService;
            var result = await controller.addNewUser(user);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
        [TestMethod]
        public async Task PostMethodFail()
        {
            User user = null;
            UsersController controller = new UsersController();
            controller.userService = userService;
            var result = await controller.addNewUser(user);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult);
        }
        [TestMethod]
        public async Task PutMethod()
        {
            var user = new User(0, "I'm an ass man", "x@gmail.com", "Phu Van M", "4Hnnv00yE4MSoqLcgtuKxgQpwQ52",
                      "https://images.unsplash.com/photo-1608848461950-0fe51dfc41cb?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8fA%3D%3D",
                      "2222222222", "FkBoy", "ePUfGbWpTC6_bR-MG6Br2h:APA91bFR0FJOuYWAM9vioIGUTLrjhKbrw5ehARex1Kt8memBzLO3S-9pe7o3dPVv2beaXextoB2M_9Sdv7g8MNEqRTTLNUxgJi8ZMNIJyfy4xWKXCB9fxnx_WM_-3fSG2GHsYN2g83Ey", 
                      "");
            UsersController controller = new UsersController();
            controller.userService = userService;
            var result = await controller.UpdateUser("4Hnnv00yE4MSoqLcgtuKxgQpwQ52", user);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
        [TestMethod]
        public async Task DeleteMethod()
        {
            User x = new User(0, "I'm a man", "x@gmail.com", "", "wes", "", "", "", "", "");
            FirebaseClient client = new FirebaseClient(testLink);
            UsersController controller = new UsersController();
            controller.userService = userService;
            await client.Child("Users").PostAsync(x);
            var data = await userService.extractData();
            var key = (from user in data where user.Value.Id == x.Id select user.Key).FirstOrDefault();
            var result = await controller.DeleteUser(key);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

    }
}
