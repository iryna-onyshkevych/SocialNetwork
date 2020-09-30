using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Models
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("firstName")]
        public string Name { get; set; }
        [BsonElement("lastName")]
        public string Surname { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("interests")]
        public List<string> Interests { get; set; }

    }
}
