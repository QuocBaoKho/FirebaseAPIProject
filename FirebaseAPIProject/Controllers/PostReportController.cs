using Firebase.Database;
using FirebaseAPIProject.Models;
using FirebaseAPIProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirebaseAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostReportController : ControllerBase
    {
        public PostReportService postReportService;

        public PostReportController()
        {
            postReportService = new PostReportService();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public async Task<ActionResult <PostReport>> AddPostReport(PostReport postReport)
        {
            try
            {
                if (postReport != null)
                {
                    var newPostReport = await postReportService.addPostReport(postReport);
                    return Ok(newPostReport);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("ExtractData")]
        public async Task<List<PostReport>> ExtractData()
        {
            var postReports = await postReportService.extractData();
            var realPostReports = (from postReport in postReports select postReport.Value).ToList();
            return realPostReports;
        }
    }
}