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
using MongoDB.Driver.Builders;
using DocumentFormat.OpenXml.Wordprocessing;
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
        public string ourId = "";//id залогованого користувача
        public List<string> ourfollowers = new List<string>();//список читачів залогованого користувача
        public List<string> ourfollowing = new List<string>();//список підписок залогованого користувача
       
            private void Info()
        {

            LoginScreen passwordWindow = new LoginScreen();
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            List<Post> posts = dataContext.Posts;
            List<Post> SortedList = posts.OrderBy(o => o.DateOfPublishing).ToList(); 
           
            TextBox text1 = new TextBox();
            text1.TextWrapping = TextWrapping.Wrap;
            int amount = 1;
            //List<Post> sortedposts; int temp;
         
            //Post newp = posts.Find(e=>e.DateOfPublishing).Where(u => u.Id == userId).FirstOrDefault();

            
            foreach (var post in posts)
            {

                //text[k] = Convert.ToInt32(post.DateOfPublishing);
                //text1.Text += post.DateOfPublishing.ToString()+ amount + " " + "Post" + "\n" + post.Name + " " + post.Surname + "\n" + post.Body + " ";
                //foreach (var post1 in post.Comments)
                //{

                //    text1.Text += "\n" + "Comments:" + "\n";
                //    for (int i = 0; i < post1.CommentBody.Count(); i++)
                //    {

                //        text1.Text += post1.CommentBody[i] + "\n" + post1.Name[i] + " " + post1.Surname[i] + "\n";
                //    }
                //    text1.Text += "\n";

                //}
                //amount++;

            }
            foreach (var post in SortedList)
            {
                MessageBox.Show(post.Name);
            }

            tab1.Items.Add(new TabItem { Header = new TextBlock { Text = "All Posts" }, Content = text1 });//додаємо дані внову вкладку

            //User FUser = users.Where(u => u.Name == Name);

            //List<string> ourfollowers = new List<string>();
            //List<string> ourfollowing = new List<string>();

            if (passwordWindow.ShowDialog() == true)
            {
                int passcount = 0;//лічильник для перевірки на правильність пароля
                foreach (var user in users)
                {
                    if (user.Password == passwordWindow.Password)//якщо введений пароль є серед паролів користувачів
                    {
                        passcount++;
                        ourId = user.Id;
                        if (user.Followers.Count != 0)
                        {
                            for (int i = 0; i < user.Followers.Count; i++)
                            {
                                ourfollowers.Add(user.Followers[i]);//шукаємо читачів залогованого користувача
                            }
                        }


                        else
                        {
                            for (int i = 0; i < user.Following.Count; i++)
                            {
                                ourfollowing.Add(user.Following[i]);//шукаємо підписки залогованого користувача
                            }
                        }
                    }
                }
                if (passcount == 1)//пароль співпав з одним із паролів в бд
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

                    ourId = user.Id;
                    MessageBox.Show(user.Name);
                }
            }
            //for (int i = 0; i < ourfollowers.Count; i++)
            //{
            //    MessageBox.Show(ourfollowers[i]);
            //}
            //for (int i = 0; i < ourfollowing.Count; i++)
            //{
            //    MessageBox.Show(ourfollowing[i]);
            //}


        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LoginScreen taskWindow = new LoginScreen();
            //taskWindow.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        public string userId = "";//id знайденого користувача
        public List<string> newfollowers = new List<string>();//список читачів знайденого користувача
        public List<string> newfollowing = new List<string>();//список підписок знайденого користувача
        public string emailus = "";//id знайденого користувача
        private void viewInfo(object sender, RoutedEventArgs e)//інформація про знайденого нами користувача
        {
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            int countus = 0;//лічильник
            foreach (var user in users)
            {

                if ((user.Name == Firstname.Text) && (user.Surname == Lastname.Text))//перевіряємо, чи дані введеного користувач є в бд
                {
                    countus++;
                    MessageBox.Show("User's info is opened in new tab");

                    TextBlock newText = new TextBlock();
                    newText.TextWrapping = TextWrapping.Wrap;
                    tab1.Items.Add(new TabItem { Header = new TextBlock { Text = user.Name + " " + user.Surname }, Content = newText });//додаємо дані внову вкладку
                    newText.Text += "Name: " + user.Name + "\n";
                    newText.Text += "Surname: " + user.Surname + "\n";
                    //newText.Text += "\n";
                    newText.Text += "Interests: ";
                    userId = user.Id;
                    emailus = user.Email;
                    for (int i = 0; i < user.Interests.Count; i++)
                    {
                        newText.Text += user.Interests[i];
                        if (i != user.Interests.Count - 1)//ставим кому, перед наступним зацікавленням, якщо воно не останнє
                            newText.Text += ", ";
                    }
                    for (int i = 0; i < user.Followers.Count; i++)//шукаємо читачів знайденого користувача
                    {
                        newfollowers.Add(user.Followers[i]);
                    }
                    for (int i = 0; i < user.Following.Count; i++)//шукаємо підписки знайденого користувача
                    {
                        newfollowing.Add(user.Following[i]);
                    }

                }
            }
            if (countus == 0)
                MessageBox.Show("There's no such user");


        }



        private void followUser(object sender, RoutedEventArgs e)
        {

            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            int check = 0;
            User founduser = users.Where(u => u.Id == userId).FirstOrDefault();
            User ouruser = users.Where(u => u.Id == ourId).FirstOrDefault();
          
            ouruser.Following.Add(founduser.Id);
            founduser.Followers.Add(ouruser.Id);

            UserService userservice = new UserService();
            userservice.Update(ourId, ouruser);
            userservice.Update(userId, founduser);
            //List<Post> post = dataContext.Posts;
            //post.ElementAtOrDefault<Post>(1).Comments.ElementAtOrDefault(1);










        }

        private void findPosts(object sender, RoutedEventArgs e)
        {
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            int check = 0;
            User founduser = users.Where(u => u.Id == userId).FirstOrDefault();
            List<Post> posts = dataContext.Posts;

            Post post1 = posts.Where(u => (u.Name == founduser.Name) && (u.Surname == founduser.Surname)).FirstOrDefault();
            TextBlock newText1 = new TextBlock();
            newText1.TextWrapping = TextWrapping.Wrap;
            tab1.Items.Add(new TabItem { Header = new TextBlock { Text = "Posts" }, Content = newText1 });//додаємо дані внову вкладку
            int amount = 1;
            foreach (var post in posts)
            {
                if ((post.Name == founduser.Name) && (post.Surname == founduser.Surname)){
                    //text1.Text = "";
                    newText1.Text += amount + " " + "Post" + "\n" + post.Name + " " + post.Surname + "\n" + post.Body + " ";
                    foreach (var post2 in post.Comments)
                    {
                        newText1.Text += "\n" + "Comments:" + "\n";
                        for (int i = 0; i < post2.CommentBody.Count(); i++)
                            newText1.Text += post2.CommentBody[i] + "\n" + post2.Name[i] + " " + post2.Surname[i] + "\n";
                        newText1.Text += "\n";

                    }
                    amount++;

                }

            }
            MessageBox.Show("User's posts are opened in new tab");
        }

        private void likePost(object sender, RoutedEventArgs e)
        {

        }

        private void addComment(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {

        }
    }
}
