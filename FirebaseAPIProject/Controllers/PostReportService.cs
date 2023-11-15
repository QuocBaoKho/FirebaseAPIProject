using Firebase.Database;
using FirebaseAPIProject.Models;
using Firebase.Database.Query;

namespace FirebaseAPIProject.Services
{
    public class PostReportService
    {
        private string dbURL = FirebaseLink.link;
        private readonly FirebaseClient client;

        public PostReportService()
        {
            client = new FirebaseClient(dbURL);
        }
        public PostReportService(FirebaseClient client)
        {
            this.client = client;
        }
        public async Task<FirebaseObject<PostReport>> addPostReport(PostReport postReport)
        {
            return await client.Child("Report").Child("Post").PostAsync(postReport);
        }
        public async Task<List<KeyValuePair<string, PostReport>>> extractData()
        {
            var postReport = await client.Child("Report").Child("Post").OnceAsync<PostReport>();
            return postReport?.Select(x => new KeyValuePair<string, PostReport>(x.Key, x.Object)).ToList();
        }      
    }
}