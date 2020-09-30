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
//using DocumentFormat.OpenXml.Spreadsheet;

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
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            //User FUser = users.Where(u => u.Name == Name);
            string ourId = "";
            if (passwordWindow.ShowDialog() == true)
            {
                int passcount = 0;
                foreach(var user in users)
                {
                    if (user.Password == passwordWindow.Password)
                    {
                        passcount++;
                        ourId += user.Id;
                    }
                }
                if (passcount == 1)
                {
                    MessageBox.Show("Autherisation is passed");
                    

                }
                else
                    MessageBox.Show("Password is incorrect");
            }
            else
            {
                MessageBox.Show("Autherisation is not passed");
            }
            foreach (var user in users)
            {
                if (user.Id == ourId)
                {
                   
                    ourId += user.Id;
                    MessageBox.Show(user.Name);
                }
            }

            //this.Closed += MainWindow_Closed;

            //foreach (var user in users)
            //{
            //    MessageBox.Show(user.Id.ToString());
            //}


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


            //TextBox newText = new TextBox();
            //newText.TextWrapping = TextWrapping.Wrap;
             //tab1.Items.Add(new TabItem { Header = new TextBlock { Text = "Tab" }, Content = newText });
             //   newText.Text = File.ReadAllText(openFileDialog.FileName);



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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void ViewInfo(object sender, RoutedEventArgs e)
        {
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            int countus = 0;
            foreach (var user in users)
            {
                
                if ((user.Name == Firstname.Text) && (user.Surname == Lastname.Text))
                {
                    countus++;
                    MessageBox.Show("User's info is opened in new tab");
                    ////var info = new TextBlock() { Content = i.ToString(), Background = new SolidColorBrush(Colors.White) };
                    ////btn.Click += Button_Click;
                    ////Grid.SetColumn(btn, value: i % 3);
                    ////Grid.SetRow(btn, value: i / 3 + 1);
                    ////grid.Children.Add(btn);
                    ////tab1.Items.Add(new TabItem { Header = new TextBlock { Text = "Tab" }, Content = newText });
                    //   newText.Text = File.ReadAllText(openFileDialog.FileName);
                    //MessageBoxResult result = MessageBox.Show("Do you want to follow this user?", "", MessageBoxButton.YesNo);
                    //switch (result)
                    //{
                    //    case MessageBoxResult.Yes:
                    //        break;
                    //    case MessageBoxResult.No:
                    //        break;
                    //}

                    TextBlock newText = new TextBlock();
                    newText.TextWrapping = TextWrapping.Wrap;
                    tab1.Items.Add(new TabItem { Header = new TextBlock { Text = user.Name + " " + user.Surname }, Content = newText });
                    newText.Text += "Name: " + user.Name + "\n";
                    newText.Text += "Surname: " + user.Surname + "\n";
                    //newText.Text += "\n";
                    newText.Text += "Interests:";
                    for (int i = 0; i < user.Interests.Count; i++)
                    {
                        newText.Text += user.Interests[i];
                        if (i != user.Interests.Count - 1)
                            newText.Text += ", ";
                    }


                }
            }
            if (countus == 0)
                MessageBox.Show("There's no such user");
        }

        private void FindUs(object sender, RoutedEventArgs e)
        {

        }
    }
}
