using Firebase.Database;
using FirebaseAPIProject.Models;
using Firebase.Database.Query;

namespace FirebaseAPIProject.Services
{
    public class UserReportService
    {
        private string dbURL = FirebaseLink.link;
        private readonly FirebaseClient client;

        public UserReportService()
        {
            client = new FirebaseClient(dbURL);
        }
        public UserReportService(FirebaseClient client)
        {
            this.client = client;
        }
        public async Task<FirebaseObject<UserReport>> addUserReport(UserReport userReport)
        {
            return await client.Child("Report").Child("User").PostAsync(userReport);
        }
        public async Task<List<KeyValuePair<string, UserReport>>> extractData()
        {
            var userReport = await client.Child("Report").Child("User").OnceAsync<UserReport>();
            return userReport?.Select(x => new KeyValuePair<string, UserReport>(x.Key, x.Object)).ToList();
        }
    }
}