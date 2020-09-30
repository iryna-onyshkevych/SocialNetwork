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
    public class UserService : IUserService
    {
        private string ConnectionString = "mongodb://localhost:27017";
        private string DataBaseName = "socialnetwork";
        private string CollectionName = "users";
        MongoDBHelper dBHelper;
        public UserService()
        {
            dBHelper = MongoDBHelper.CreateInstance(ConnectionString, DataBaseName);
        }
        public void Create(User user)
        {
            dBHelper.InsertDocument<User>(CollectionName, user);
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
