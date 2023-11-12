using Firebase.Database;
using FirebaseAPIProject.Models;
using Firebase.Database.Query;
namespace FirebaseAPIProject.Services
{
    public class PostService
    {
        private string dbURL = FirebaseLink.link;
        private readonly FirebaseClient client;
        public PostService()
        {
            client = new FirebaseClient(dbURL);

        }
        public PostService(FirebaseClient _client)
        {
            client = _client;
        }
        public async Task<FirebaseObject<Post>> postNew(Post post)
        {

           var x =  await client.Child("Posts").PostAsync(post);
            return x;
        }
        public async Task putNew(string id, Post post)
        {
            await client.Child("Posts").Child(id).PutAsync(post);
           
        }
        public async Task<List<KeyValuePair<string, Post>>> extractData()
        {
            var posts = await client.Child("Posts").OnceAsync<Post>();
            return posts.Select(x => new KeyValuePair<string, Post>(x.Key, x.Object)).ToList();
        }
    }
}

