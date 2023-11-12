using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITest.Interface;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAPIProject.Models;
using FirebaseAPIProject.Services;
namespace APITest.Class
{
    internal class FirebaseWrapper : IFirebaseWrapper
    {
        
        public IEnumerable<KeyValuePair<string, Post>> Select(FirebaseClient client)
        {
            return client.Child("Posts").OnceAsync<Post>().Result.Select(x => new KeyValuePair<string, Post>());
        }
    }
}
