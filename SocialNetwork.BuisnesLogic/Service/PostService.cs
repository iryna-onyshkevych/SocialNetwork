using SocialNetwork.BuisnesLogic.Interfaces;
using SocialNetwork.DataAccess.Helpers;
using SocialNetwork.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BuisnesLogic.Service
{
    public class PostService : IPostsService
    {
        private string ConnectionString = "mongodb://localhost:27017";
        private string DataBaseName = "socialnetwork";
        private string CollectionName = "posts";
        MongoDBHelper dBHelper;
        public PostService()
        {
            dBHelper = MongoDBHelper.CreateInstance(ConnectionString, DataBaseName);
        }
        public void Create(Post post)
        {
            dBHelper.InsertDocument<Post>(CollectionName, post);
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPosts()
        {
            return dBHelper.LoadAllDocuments<Post>(CollectionName);
        }

        public Post GetPosts(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Post post)
        {
            dBHelper.UpdateDocument<Post>(CollectionName, id, post);
        }
    }
}
