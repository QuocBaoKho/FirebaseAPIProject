using System.Xml.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAPIProject.Models;

namespace FirebaseAPIProject.Services
{
    public class CommentService
    {
        private string dbURL = FirebaseLink.link;
        private readonly FirebaseClient client;
        public CommentService()
        {
            client = new FirebaseClient(dbURL);

        }
        public CommentService(FirebaseClient client)
        {
            this.client = client;
        }
        public async Task<List<KeyValuePair<string, Comments>>> getPost_sComments(string postId)
        {
            var comments = await client.Child("Comments").Child(postId).OnceAsync<Comments>();
            return comments.Select(x=> new KeyValuePair<string, Comments>(x.Key, x.Object)).ToList();
        }
        public async Task <FirebaseObject<Comments>> PostComment(string postId, Comments comment)
        {
            var x = await client.Child("Comments").Child(postId).PostAsync(comment);
            var commentData = await getPost_sComments(postId);
            var value = (from item in commentData where item.Value.CommentId == comment.CommentId select item.Value).FirstOrDefault();
            var key = (from item in commentData where item.Value.CommentId == comment.CommentId select item.Key).FirstOrDefault();
            value.CommentId = key;
            await UpdateComment(postId, key, value);
            return x;
        }
        public async Task UpdateComment(string postId, string commentId, Comments comment)
        {
            await client.Child("Comments").Child(postId).Child(commentId).PutAsync(comment);
        }
        public async Task<string> DeleteComment(string postId, string commentId)
        {
            await client.Child("Comments").Child(postId).Child(commentId).DeleteAsync();
            var commentData = await getPost_sComments(postId);
            var key = (from item in commentData where item.Value.CommentId == commentId select item.Key).FirstOrDefault();
            return key;
        }

    }
}
