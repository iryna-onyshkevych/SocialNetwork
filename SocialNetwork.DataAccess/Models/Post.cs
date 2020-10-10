using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }

        [BsonElement("likes")]
        public int Like { get; set; }
        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("surname")]
        public string Surname { get; set; }
        [BsonElement("dateofpublishing")]
        public DateTime DateOfPublishing { get; set; }

       
    }
    
    public class Comment
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        [BsonElement("body")]
        public List<string> CommentBody { get; set; }
        [BsonElement("name")]
        public List<string> Name { get; set; }
   
        [BsonElement("surname")]
        public List<string> Surname { get; set; }
      
        //[BsonElement("surname")]
        //public string Surname { get; set; }


    }
   
}
