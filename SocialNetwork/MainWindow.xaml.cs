using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

    }
    class Comment
    {
        [BsonElement("body")]
        public string CommentBody { get; set; }
        [BsonElement("users")]
        public string User { get; set; }
        [BsonElement("date")]
        public object CommentCreateDate { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("socialnetwork");
            IMongoCollection<BsonDocument> postBsonColection = database.GetCollection<BsonDocument>("users");
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq("age", 45);
            var postBSON = postBsonColection.Find(filter).ToList();
            int b = postBSON.Count();
            MessageBox.Show(b.ToString());
            var newPost = new Post();
        }
    }
}
