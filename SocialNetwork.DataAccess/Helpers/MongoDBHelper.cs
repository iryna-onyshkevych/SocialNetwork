using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Helpers
{
     public class MongoDBHelper
    {
        private readonly IMongoDatabase _db;
        public static MongoDBHelper CreateInstance(string connectionString, string databaseName)
        {
            return new MongoDBHelper(connectionString, databaseName);
        }


        private MongoDBHelper(string connectionString, string databaseName)
        {
            //Create new database connection
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
        }

        public void InsertDocument<T>(string collectionName, T document)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertOne(document);
        }

        public List<T> LoadAllDocuments<T>(string collectionName)
        {
            var collection = _db.GetCollection<T>(collectionName);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadDocumentById<T>(string collectionName, Guid id)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpdateDocument<T>(string collectionName, Guid id, T document)
        {
            var collection = _db.GetCollection<T>(collectionName);

            var result = collection.ReplaceOne(
           new BsonDocument("_id", id),
           document,
           new UpdateOptions { IsUpsert = false });
        }

        public void UpsertDocument<T>(string collectionName, Guid id, T document)
        {
            var collection = _db.GetCollection<T>(collectionName);

            if (collection != null)
            {
                var result = collection.ReplaceOne(new BsonDocument("_id", id), document,
                new UpdateOptions { IsUpsert = true });
            }
        }

        public void DeleteDocument<T>(string collectionName, Guid id)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);

        }

    }
}
