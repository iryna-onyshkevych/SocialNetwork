using SocialNetwork.DataAccess.Helpers;
using SocialNetwork.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Contest
{
    public class DataContest
    {
        public List<User> Users;
        public List<Post> Posts;
        public DataContest()
        {
            var ConnectionString = "mongodb://localhost:27017";
            var DataBaseName = "socialnetwork";
            MongoDBHelper mongoDb = MongoDBHelper.CreateInstance(ConnectionString, DataBaseName);
            Users = mongoDb.LoadAllDocuments<User>("users");
            
            //Posts = mongoDb.LoadAllDocuments<Post>("posts");
        }
    }
}
