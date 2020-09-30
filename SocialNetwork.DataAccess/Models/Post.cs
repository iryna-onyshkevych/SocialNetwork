using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Models
{
   public  class Post
    {
        [BsonId]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }
        [BsonElement("likes")]
        public List<Like> Likes { get; set; }

    }
    public class Comment
    {
        [BsonElement("body")]

        public string Commentbody { get; set; }
        [BsonElement("user")]
        public string User { get; set; }
        [BsonElement("date")]
        public object CommentCreatedDate { get; set; }

    }
    public class Like
    {
        [BsonElement("amount")]

        public int LikesAmount { get; set; }
        [BsonElement("userslike")]
        public string UsersLike { get; set; }
       

    }
}
