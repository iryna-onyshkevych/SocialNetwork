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
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("surname")]
        public string Surname { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("interest")]
        public List<string> Interests { get; set; }

    }
}
