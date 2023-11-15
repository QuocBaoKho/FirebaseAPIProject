using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAPIProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirebaseAPIProject.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostReportController : ControllerBase
    {
        private readonly FirebaseClient _firebaseClient;

        public PostReportController(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        [HttpPost("{postId}/report")]
        public ActionResult<string> ReportPost(string postId, [FromBody] PostReport postReport)
        {
            // Gửi báo cáo đến Firebase Realtime Database
            var reportsRef = _firebaseClient.Child("posts").Child(postId).Child("reports");
            reportsRef.PostAsync(postReport);

            // Trả về thông báo thành công nếu không có lỗi
            return Ok("Báo cáo đã được gửi thành công!");
        }

        [HttpGet("{postId}/reports")]
        public async Task<ActionResult<PostReport[]>> GetPostReports(string postId)
        {
            // Đọc dữ liệu báo cáo từ Firebase Realtime Database
            var reportsRef = _firebaseClient.Child("posts").Child(postId).Child("reports");
            var reportsSnapshot = await reportsRef.OnceAsync<PostReport>();

            var postReports = new List<PostReport>();
            foreach (var reportSnapshot in reportsSnapshot)
            {
                var postReport = reportSnapshot.Object;
                postReports.Add(postReport);
            }

            // Trả về danh sách báo cáo
            return Ok(postReports.ToArray());
        }
    }
}