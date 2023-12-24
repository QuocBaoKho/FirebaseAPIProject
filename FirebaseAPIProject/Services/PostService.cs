using Firebase.Database;
using FirebaseAPIProject.Models;
using Firebase.Database.Query;
using Microsoft.Extensions.Hosting;

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
            if(_client is null)
            {
                _client = new FirebaseClient(dbURL);
            }
            client = _client;
        }
        public async Task<FirebaseObject<Post>> postNew(Post post)
        {

           var x =  await client.Child("Posts").PostAsync(post);
            var data = await extractData();
            var user = (from post1 in data where post1.Value.Postid == post.Postid select post1.Value).FirstOrDefault();
            var key = (from post1 in data where post1.Value.Postid == post.Postid select post1.Key).FirstOrDefault();
            user.Postid = key;
            await putNew(key, user);
            return x;
        }
        public async Task<string> putNew(string id, Post post)
        {
            await client.Child("Posts").Child(id).PutAsync(post);
            var data = await extractData();
            var key = (from post1 in data where post1.Value.Postid == post.Postid select post1.Key).FirstOrDefault();
            return key;
        }
        public async Task<List<KeyValuePair<string, Post>>> extractData()
        {
            var posts = await client.Child("Posts").OnceAsync<Post>();
            return posts.Select(x => new KeyValuePair<string, Post>(x.Key, x.Object)).ToList();
        }
        public async Task<string> DeletePost(string id)
        {
            await client.Child("Posts").Child(id).DeleteAsync();
            var data = await extractData();
            var key = (from post1 in data where post1.Value.Postid == id select post1.Key).FirstOrDefault();
            key = key is null ? "" : key;
            return key;
        }
    }
}

