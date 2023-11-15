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
           var x =  await client.Child("Users").PostAsync(user);
            var data = await extractData();
            var user1 = (from e in data where e.Value.Id == user.Id select e.Value).FirstOrDefault();
            var key = (from e in data where e.Value.Id == user.Id select e.Key).FirstOrDefault();
            user1.Id = key;
            await putNew(key, user1);
            return x;
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
        public async Task<string> putNew(string id, User user)
        {
            await client.Child("Users").Child(id).PutAsync(user);
            var data = await extractData();
            var key = (from e in data where e.Value.Id == user.Id select e.Key).FirstOrDefault();
            return key;

        }
        public async Task<string> deleteUser(string id)
        {
            await client.Child("Users").Child(id).DeleteAsync();
            var data = await extractData();
            var key = (from e in data where e.Value.Id == id select e.Key).FirstOrDefault();
            return key;
        }
    }
}
