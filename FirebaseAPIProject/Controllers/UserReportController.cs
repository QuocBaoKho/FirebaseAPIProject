using Firebase.Database;
using FirebaseAPIProject.Models;
using FirebaseAPIProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirebaseAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReportController : ControllerBase
    {
        public UserReportService userReportService;
        public UserReportController()
        {
            userReportService = new UserReportService();
        }
        public UserReportController(UserReportService userReportService)
        {
            this.userReportService = userReportService;
        }

        [HttpPost]
        public async Task<ActionResult<UserReport>> AddUserReport(UserReport userReport)
        {
            var result = await userReportService.addUserReport(userReport);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<UserReport>> ExtractData()
        {
            var result = await userReportService.extractData();
            return Ok(result);
        }
    }
}
