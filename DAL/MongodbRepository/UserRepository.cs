using DAL.Enteties;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DAL.Repository
{
    public class UserRepository
    {
        IMongoDatabase database;
        IMongoCollection<User> collection;
        public UserRepository()
        {
            database = Program.GetDefaultDatabase();
            collection = database.GetCollection<User>(GetTableName());
        }

        private string GetTableName()
        {
            return "users";
        }
        public void addFollower(string userlog, string newFollower)
        {
            var filter = Builders<User>.Filter.Eq("Userlog", userlog);
            var update = Builders<User>.Update.Push("Followers", newFollower);
            collection.UpdateOne(filter, update);

        }
        public void unFollow(string userlog, string follower)
        {
            var filter = Builders<User>.Filter.Eq("Userlog", userlog);
            var update = Builders<User>.Update.Pull("Following", follower);
            collection.UpdateOne(filter, update);


            filter = Builders<User>.Filter.Eq("Userlog", follower);
            update = Builders<User>.Update.Pull("Followers", userlog);
            collection.UpdateOne(filter, update);
        }
        public void Add(User user) =>
           collection.InsertOne(user);

        public void Add(IEnumerable<User> entities) =>
            collection.InsertMany(entities);
        // 
        public void Update(string userlog, User user) =>
            collection.ReplaceOne(entity => entity.Userlog == userlog, user);

        public void Update(ObjectId id, User user) =>
            collection.ReplaceOne(entity => entity.Id == id, user);

        public void UpdateField(string userlog, string FieldToEdit, string FieldValue)
        {
            var filter = Builders<User>.Filter.Eq("Userlog", userlog);
            var update = Builders<User>.Update.Set(FieldToEdit, FieldValue);
            collection.UpdateOne(filter, update);
        }

        public void UpdateDate(string userlog)
        {
            var filter = Builders<User>.Filter.Eq("Userlog", userlog);
            var update = Builders<User>.Update.Set("Date", DateTime.Now.ToString());
            collection.UpdateOne(filter, update);
        }
        //
       

        public void addFollowing(string userlog, string newFollowing)
        {
            var filter = Builders<User>.Filter.Eq("Userlog", userlog);
            var update = Builders<User>.Update.Push("Following", newFollowing);
            collection.UpdateOne(filter, update);

        }
        //
        public List<string> GetFollowers(string userlog)
        {
            var filter = Builders<User>.Filter.Eq("Userlog", userlog);
            var people = collection.Find(filter).Project(x => x.Followers).First();
            return people;
        }

        public List<string> GetFollowing(string userlog)
        {
            var filter = Builders<User>.Filter.Eq("Userlog", userlog);
            var people = collection.Find(filter).Project(x => x.Following).First();
            return people;
        }
        //
        public ObjectId GetUserId(string userlog)
        {
            var user = collection.Find(entity => entity.Userlog == userlog).FirstOrDefault();
            return user.Id;
        }
        //
        public List<User> GetUsers() =>
            collection.Find(entity => true).ToList();

        public User GetUser(string userlog) =>
          collection.Find(entity => entity.Userlog == userlog).FirstOrDefault();

        public User GetUser(ObjectId id) =>
         collection.Find(entity => entity.Id == id).FirstOrDefault();
        //

        public void Delete(User user) =>
             collection.DeleteOne(u => u.Id == user.Id);

        public void Delete(ObjectId userId) =>
            collection.DeleteOne(u => u.Id == userId);

        public void Delete(string userlog) =>
            collection.DeleteOne(u => u.Userlog == userlog);

    }
}
