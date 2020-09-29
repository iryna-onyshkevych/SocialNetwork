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
    class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("surname")]
        public string Surname { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("interests")]
        public string[] Interests { get; set; }

    }
    //class Interests
    //{
    //    [BsonElement("body")]
    //    public string CommentBody { get; set; }
    //    [BsonElement("users")]
    //    public string User { get; set; }
    //    [BsonElement("date")]
    //    public object CommentCreateDate { get; set; }
    //}

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("socialnetwork");
            IMongoCollection<BsonDocument> postsBsonCollection = database.GetCollection<BsonDocument>("users");
            //var filterBuilder = Builders<BsonDocument>.Filter;
            //var filter = filterBuilder.Eq("age", 45);
            //var postBSON = postsBsonCollection.Find(filter).ToList();
            //var posts = postsBsonCollection.Find(filter).Project("{title:1").ToList();
            //int b = postBSON.Count();
            //MessageBox.Show(b.ToString());
            var newPost = new User();
            //newPost.Name = String.Format("Oksana", DateTime.Now.ToString());
            //newPost.Interests = new string[] { "bowling" };
            IMongoCollection<User> postsCollection = database.GetCollection<User>("users");
            //postsCollection.InsertOne(newPost);
            newPost.Name = ("Olena");
            postsCollection.ReplaceOne(p => p.Id == newPost.Id, newPost);
            postsCollection.DeleteOne(p => p.Id == newPost.Id);
            var courses = database.GetCollection<User>("users");
            //var course = courses.FindAllAs<users>().SetFields(Fields.Include("Title", "Description").Exclude("_id")).ToList(); 

            //posts1.Text += course;

            //List<User> list = postsCollection.AsQueryable().ToList<User>();
            //var posts = from p in list select p;
            //foreach (User p in posts)
            //{
            //    Console.WriteLine(p.Name);
            //}

        }
        
    }
}
