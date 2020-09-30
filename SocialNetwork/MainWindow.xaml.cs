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
using DocumentFormat.OpenXml.Spreadsheet;
using SocialNetwork.DataAccess.Helpers;
using SocialNetwork.BuisnesLogic.Service;
//using DocumentFormat.OpenXml.Spreadsheet;

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
  

    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
           
            InitializeComponent();
            Info();
            
          


        }
        public string ourId = "";
        public List<string> ourfollowers = new List<string>();
        public List<string> ourfollowing = new List<string>();
        private void Info()
        {
            LoginScreen passwordWindow = new LoginScreen();
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            //User FUser = users.Where(u => u.Name == Name);
            
            //List<string> ourfollowers = new List<string>();
            //List<string> ourfollowing = new List<string>();

            if (passwordWindow.ShowDialog() == true)
            {
                int passcount = 0;
                foreach (var user in users)
                {
                    if (user.Password == passwordWindow.Password)
                    {
                        passcount++;
                        ourId += user.Id;
                        for (int i = 0; i < user.Followers.Count; i++)
                        {
                            ourfollowers.Add(user.Followers[i]);
                        }
                        for (int i = 0; i < user.Following.Count; i++)
                        {
                            ourfollowing.Add(user.Following[i]);
                        }
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
            for (int i = 0; i < ourfollowers.Count; i++)
            {
                MessageBox.Show(ourfollowers[i]);
            }
            for (int i = 0; i < ourfollowing.Count; i++)
            {
                MessageBox.Show(ourfollowing[i]);
            }
            

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LoginScreen taskWindow = new LoginScreen();
            //taskWindow.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


        public List<string> newfollowers = new List<string>();
        public List<string> newfollowing = new List<string>();
        private void viewInfo(object sender, RoutedEventArgs e)
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
                    for (int i = 0; i < user.Followers.Count; i++)
                    {
                        newfollowers.Add(user.Followers[i]);
                    }
                    for (int i = 0; i < user.Following.Count; i++)
                    {
                        newfollowing.Add(user.Following[i]);
                    }

                }
            }
            if (countus == 0)
                MessageBox.Show("There's no such user");

            
        }

        private void findPosts(object sender, RoutedEventArgs e)
        {

        }

        private void followUser(object sender, RoutedEventArgs e)
        {

            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            int check = 0;
            for (int i = 0; i < newfollowing.Count; i++)
            {
                if (ourId == newfollowing[i])
                    check++;
            }
            if (check == 0)
            {

                //List<User> users = database.GetCollection<User>("users");
                //var user = new User();
                //user.Name = "New";
                ////newPost.Interests = new string[] { "bowling" };
                //IMongoCollection<User> postsCollection = database.GetCollection<User>("users");
                //postsCollection.InsertOne(user);
                var user1 = new User();
                
                user1.Followers[newfollowers.Count + 1] = ourId;
                UserService userService = new UserService();
                userService.Create(user1);

            }


        }
    }
}
