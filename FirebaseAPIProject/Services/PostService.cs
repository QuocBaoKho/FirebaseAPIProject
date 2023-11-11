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

        public async Task postNew(Post post)
        {

            await client.Child("Posts").PostAsync(post);
        }
        public async Task<List<KeyValuePair<string, Post>>> extractData()
        {
            var posts = await client.Child("Posts").OnceAsync<Post>();
            return posts?.Select(x => new KeyValuePair<string, Post>(x.Key, x.Object)).ToList();
        }
    }
}

