using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Program
    {
        private Program()
        {
        }

        public static IMongoDatabase GetDefaultDatabase()
        {
            var connectionString = GetDefaultConnectionString();
            var client = new MongoClient(connectionString);
            return client.GetDatabase(GetDefaultDatabaseName());
        }

        private static string GetDefaultConnectionString()
        {
            return "mongodb://localhost:27017";
        }


        private static string GetDefaultDatabaseName()
        {
            return "socialNetwork";
        }


    }
}
