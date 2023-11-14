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
        public UserService(FirebaseClient client)
        {
            this.client = client;  
        }
        public async Task<FirebaseObject<User>> addUser(User user)
        {
           return  await client.Child("Users").PostAsync(user);
        }
        public async Task<List<KeyValuePair<string, User>>> extractData()
        {
            var user = await client.Child("Users").OnceAsync<User>();
            return user?.Select(x => new KeyValuePair<string, User>(x.Key, x.Object)).ToList();
        }
        public async Task<User> getUserviaID(string id)
        {
            var user = await client.Child("Users").Child(id).OnceSingleAsync<User>();
            return user;

        }
        public async Task putNew(string id, User user)
        {
            await client.Child("Users").Child(id).PutAsync(user);

        }
    }
}
