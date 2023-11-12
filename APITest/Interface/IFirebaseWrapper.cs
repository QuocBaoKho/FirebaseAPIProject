using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using FirebaseAPIProject.Models;

namespace APITest.Interface
{
    internal interface IFirebaseWrapper
    {
        public IEnumerable<KeyValuePair<string, Post>> Select(FirebaseClient client);
    }
}
