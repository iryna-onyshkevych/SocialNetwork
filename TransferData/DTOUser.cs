using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataTransfer.Models
{
    public class DTOUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("UserLogin")]
        public string UserLogin { get; set; }

        [BsonElement("UserPassword")]
        public string UserPassword { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Interests")]
        public List<string> Interests { get; set; }
        [BsonElement("FriendsId")]
        public List<int> FriendsId { get; set; }

        [BsonElement("RegisterDate")]
        public BsonTimestamp RegisterDate { get; set; }
    }
}
