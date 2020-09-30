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
using SocialNetwork.DataAccess.Models;
using SocialNetwork.DataAccess.Contest;

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
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
            LoginScreen passwordWindow = new LoginScreen();

            if (passwordWindow.ShowDialog() == true)
            {
                if (passwordWindow.Password == "12345678")
                    MessageBox.Show("Autherisation is passed");
                else
                    MessageBox.Show("Password is incorrect");
            }
            else
            {
                MessageBox.Show("Autherisation is not passed");
            }
            //this.Closed += MainWindow_Closed;
            //DataContest dataContext = new DataContest();
            //List<User> users = dataContext.Users;
            //foreach (var user in users)
            //{
            //    MessageBox.Show(user.Id.ToString());
            //}
            //User FUser = users.Where(u => u.Name == Nae && u.Pass);

            ////Console.ReadLine();
            //DataContest dataContext = new DataContest();
            //List<User> users = dataContext.Users;
            //foreach (var user in users)
            //{
            //    MessageBox.Show(user.Id.ToString());
            //}
            //var client = new MongoClient("mongodb://localhost:27017");
            //var database = client.GetDatabase("socialnetwork");
            //IMongoCollection<BsonDocument> postsBsonCollection = database.GetCollection<BsonDocument>("users");


            ////var filterBuilder = Builders<BsonDocument>.Filter;
            ////var filter = filterBuilder.Eq("age", 453
            ////var postBSON = postsBsonCollection.Find(filter).ToList();
            ////var posts = postsBsonCollection.Find(filter).Project("{title:1").ToList();
            ////int b = postBSON.Count();
            ////MessageBox.Show(b.ToString());
            //List<User> users = database.GetCollection<User>("users");
            //var user = new User();
            //user.Name = "New";
            ////newPost.Interests = new string[] { "bowling" };
            //IMongoCollection<User> postsCollection = database.GetCollection<User>("users");
            //postsCollection.InsertOne(user);
            ////newPost.Name = ("Olena");
            ////postsCollection.ReplaceOne(p => p.Id == newPost.Id, newPost);
            ////postsCollection.DeleteOne(p => p.Id == newPost.Id);
            ////var courses = database.GetCollection<User>("users");
            ////var course = courses.FindAllAs<users>().SetFields(Fields.Include("Title", "Description").Exclude("_id")).ToList(); 

            //var documents = postsBsonCollection.Find(new BsonDocument()).ToList();

            //foreach (BsonDocument doc in documents)
            //{

            //    MessageBox.Show(doc[2].ToString());
            //}
            ////var collection = database.GetCollection<BsonDocument>("users");
            ////var first = postsBsonCollection.Find(p => true).ToListAsync().Result.FirstOrDefault();
            ////MessageBox.Show(first.ToString());






        }
        //void MainWindow_Closed(object sender, EventArgs e)
        //{
        //    App.Current.Shutdown();
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LoginScreen taskWindow = new LoginScreen();
            //taskWindow.Show();
        }
    }
}
