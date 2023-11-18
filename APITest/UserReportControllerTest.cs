using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using FirebaseAPIProject.Controllers;
using FirebaseAPIProject.Models;
using FirebaseAPIProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace APITest
{
    [TestClass]
    public class UserReportControllerTest
    {
        string testLink = "https://friendversetest-default-rtdb.asia-southeast1.firebasedatabase.app/";
        UserReportService userReportService;
        public UserReportControllerTest()
        {
            FirebaseClient client = new FirebaseClient(testLink);
            userReportService = new UserReportService(client);
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
            List<KeyValuePair<string, UserReport>> expectedResult = await userReportService.extractData();
            List<UserReport> expectedValues = (from userReport in expectedResult select userReport.Value).ToList();
            UserReportController controller = new UserReportController();
            controller.userReportService = userReportService;

            var result = await controller.ExtractData();
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult)); ;


        }
        [TestMethod]
        public async Task PostMethod()
        {
            var newUserReport = new UserReport("-NX3tiWvU4g753fwTJaM", "dmmm", "-NWaDpnpzob3Ue68T6Gt", "tRDwJaJRQNfghpKaFsPH6MtoBcW2", " ");
            UserReportController controller = new UserReportController(); 
            controller.userReportService = userReportService;
            var result = await controller.AddUserReport(newUserReport);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
        [TestMethod]
        public async Task PostMethod2()
        {
            var newUserReport = new UserReport("-NX3tiWvU4g753fwTJaM", "He is very rude", "-NWaDpnpzob3Ue68T6Gt", "tRDwJaJRQNfghpKaFsPH6MtoBcW2", "Man");
            UserReportController controller = new UserReportController();
            controller.userReportService = userReportService;
            var result = await controller.AddUserReport(newUserReport);

            var objectResult = GetStatusCode(result);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
    }
}
