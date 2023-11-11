using Firebase.Database;
using FirebaseAPIProject.Models;
using Firebase.Database.Query;

namespace FirebaseAPIProject.Services
{
    public class UserService
    {
        private string dbURL = FirebaseLink.link;
        private readonly FirebaseClient client;

        public UserService()
        {
            client = new FirebaseClient(dbURL);
        }
        public async Task addUser(User user)
        {
            await client.Child("Users").PostAsync(user);
        }
        public async Task<List<KeyValuePair<string, User>>> extractData()
        {
            var user = await client.Child("Users").OnceAsync<User>();
            return user?.Select(x => new KeyValuePair<string, User>(x.Key, x.Object)).ToList();
        }
    }
}
