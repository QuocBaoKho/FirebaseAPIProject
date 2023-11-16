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
    public class PostReportControllerTest
    {
        string testLink = "https://friendversetest-default-rtdb.asia-southeast1.firebasedatabase.app/";
        PostReportService postReportService;
        public PostReportControllerTest()
        {
            FirebaseClient client = new FirebaseClient(testLink);
            postReportService = new PostReportService(client);
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
            List<KeyValuePair<string, PostReport>> expectedResult = await postReportService.extractData();
            List<PostReport> expectedValues = (from postReport in expectedResult select postReport.Value).ToList();
            PostReportController controller = new PostReportController();
            controller.postReportService = postReportService;

            List<PostReport> result = await controller.ExtractData();

            CollectionAssert.AreEqual(expectedValues, result);


        }
        [TestMethod]
        public async Task PostMethod()
        {
            var newPostReport = new PostReport("-NX3tiWvU4g753fwTJaM", "dmmm", "-NWaDpnpzob3Ue68T6Gt", "tRDwJaJRQNfghpKaFsPH6MtoBcW2", " ");
            PostReportController controller = new PostReportController();
            controller.postReportService = postReportService;
            var result = await controller.AddPostReport(newPostReport);
            var objectResult = GetStatusCode(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(StatusCodes.Status200OK, objectResult);
        }
        

    }
}