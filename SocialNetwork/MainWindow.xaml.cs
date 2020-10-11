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
using SocialNetwork.DataAccess.Helpers;
using SocialNetwork.BuisnesLogic.Service;
using MongoDB.Driver.Builders;

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
        public string ourname = "";//id залогованого користувача
        public string oursurname = "";//id залогованого користувача
        public List<string> ourfollowers = new List<string>();//список читачів залогованого користувача
        public List<string> ourfollowing = new List<string>();//список підписок залогованого користувача

        private void Info()
        {

            LoginScreen passwordWindow = new LoginScreen();
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            List<Post> posts = dataContext.Posts;
            List<Post> SortedList = posts.OrderByDescending(o => o.DateOfPublishing).ToList(); //посортували пости за датою
           
            TextBox text1 = new TextBox();
            text1.TextWrapping = TextWrapping.Wrap;
            int amount = 1;
           
            foreach (var post in SortedList)
            {

                text1.Text += "\n" +  amount + " " + "Post" + " likes:" + post.Like.ToString()+ "\n" + post.Name + " " + post.Surname + "\n" + post.Body + "\n";
                foreach (var post1 in post.Comments)
                {

                    text1.Text += "Comment:" + "\n";
                    text1.Text += post1.CommentBody + "\n" + post1.Name + " " + post1.Surname + "\n";

                }
                text1.Text += "\n";
                amount++;

            }
           
            tab1.Items.Add(new TabItem { Header = new TextBlock { Text = "All Posts" }, Content = text1 });//додаємо дані внову вкладку

            if (passwordWindow.ShowDialog() == true)
            {
                int passcount = 0;//лічильник для перевірки на правильність пароля
                foreach (var user in users)
                {
                    if (user.Password == passwordWindow.Password)//якщо введений пароль є серед паролів користувачів
                    {
                        passcount++;
                        ourId = user.Id;
                        ourname = user.Name;
                        oursurname = user.Surname;
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
        public string username = "";//id знайденого користувача
        public string usersurname = "";//id знайденого користувача

        public List<string> newfollowers = new List<string>();//список читачів знайденого користувача
        public List<string> newfollowing = new List<string>();//список підписок знайденого користувача
        public string emailus = "";//id знайденого користувача
        private void ViewInfo(object sender, RoutedEventArgs e)//інформація про знайденого нами користувача
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
                    newText.Text += "Interests: ";
                    userId = user.Id;
                    emailus = user.Email;
                    username = user.Name;
                    usersurname = user.Surname;
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



        private void FollowUser(object sender, RoutedEventArgs e)
        {

            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            User founduser = users.Where(u => u.Id == userId).FirstOrDefault();
            User ouruser = users.Where(u => u.Id == ourId).FirstOrDefault();

            ouruser.Following.Add(founduser.Id);
            founduser.Followers.Add(ouruser.Id);

            UserService userservice = new UserService();
            userservice.Update(ourId, ouruser);
            userservice.Update(userId, founduser);


        }

        private void FindPosts(object sender, RoutedEventArgs e)
        {
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            User founduser = users.Where(u => u.Id == userId).FirstOrDefault();
            List<Post> posts = dataContext.Posts;

            Post post1 = posts.Where(u => (u.Name == founduser.Name) && (u.Surname == founduser.Surname)).FirstOrDefault();
            TextBox newText1 = new TextBox();
            newText1.TextWrapping = TextWrapping.Wrap;
            tab1.Items.Add(new TabItem { Header = new TextBlock { Text = "Posts" }, Content = newText1 });//додаємо дані внову вкладку
            int amount = 1;
            foreach (var post in posts)
            {
                if ((post.Name == founduser.Name) && (post.Surname == founduser.Surname)){
                    //text1.Text = "";
                    newText1.Text += amount + " " + "Post" + " likes:" + post.Like.ToString()+"\n" + post.Name + " " + post.Surname + "\n" + post.Body + " ";
                    foreach (var post2 in post.Comments)
                    {
                        newText1.Text += "\n" + "Comments:" + "\n";
                        newText1.Text += post2.CommentBody+ "\n" + post2.Name + " " + post2.Surname + "\n";
                        newText1.Text += "\n";

                    }
                    amount++;

                }

            }
            MessageBox.Show("User's posts are opened in new tab");
        }

        private void LikePost(object sender, RoutedEventArgs e)
        {
            string postId = "";
            DataContest dataContext = new DataContest();
            List<Post> posts = dataContext.Posts;
           
           
            List<Post> SortedList = posts.OrderByDescending(o => o.DateOfPublishing).ToList(); //посортували пости за датою
            for (int i = 0; i < SortedList.Count; i++)
                postId = SortedList[Convert.ToInt32(likespostnumber.Text) - 1].Id;
            Post newcom = posts.Where(u => u.Id == postId).FirstOrDefault();
            newcom.Like++;
            PostService postservice = new PostService();
            postservice.Update(postId, newcom);
        }

       

        private void Comment(object sender, RoutedEventArgs e)
        {
            string postId = "";
            DataContest dataContext = new DataContest();
            List<Post> posts = dataContext.Posts;
            List < User > users = dataContext.Users;
            User founduser = users.Where(u => u.Id == ourId).FirstOrDefault();
            Comment comment1 = new Comment();
            comment1.Name = founduser.Name;
            comment1.Surname = founduser.Surname;
            comment1.CommentBody = comment.Text;
            List<Post> SortedList = posts.OrderByDescending(o => o.DateOfPublishing).ToList(); //посортували пости за датою
            for (int i = 0; i < SortedList.Count; i++)
                postId = SortedList[Convert.ToInt32(postnumber.Text)-1].Id;
            Post newcom = posts.Where(u => u.Id == postId).FirstOrDefault();
            newcom.Comments.Add(comment1);

            PostService postservice = new PostService();
            postservice.Update(postId, newcom);

        }

    

        private void Commentuserspost(object sender, RoutedEventArgs e)
        {
            string postId = "";
            DataContest dataContext = new DataContest();
            List<Post> posts = dataContext.Posts;
            List<User> users = dataContext.Users;
            User founduser = users.Where(u => u.Id == ourId).FirstOrDefault();
            Comment comment1 = new Comment();
            comment1.Name = founduser.Name;
            comment1.Surname = founduser.Surname;
            comment1.CommentBody = usercomment.Text;
            List<Post> UsersPost = posts.Where(u => u.Name == username && u.Surname ==usersurname).ToList(); //посортували пости за датою
            for (int i = 0; i < UsersPost.Count; i++)
                postId = UsersPost[Convert.ToInt32(userpostnumber.Text)-1].Id;
            Post newcom = posts.Where(u => u.Id == postId).FirstOrDefault();
            newcom.Comments.Add(comment1);

            PostService postservice = new PostService();
            postservice.Update(postId, newcom);
        }

        private void Post(object sender, RoutedEventArgs e)
        {
            DataContest dataContext = new DataContest();
            List<Post> posts = dataContext.Posts;
            Post newpost = new Post();
            newpost.Like = 0;
            newpost.Body = postfield.Text;
            newpost.Name = ourname;
            newpost.Surname = oursurname;
            newpost.DateOfPublishing = DateTime.Now;
            newpost.Id = "5f735b557067227c2a233264";
            List<string> ids = new List<string>();
            string newid = RandomString(24);

            foreach (var currentposts in posts)
            {
                ids.Add(currentposts.Id);
            }
            int temp = ids.Count;
            while (temp!=0)
            {
                if (ids[ids.Count - 1] == newid)
                {
                    newid = RandomString(24);
                    temp = ids.Count;
                }
                else
                    temp--;


            }

            newpost.Id = newid;
            newpost.Comments = new List<Comment> { };
            PostService postservice = new PostService();
            postservice.Create(newpost);

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

      

        private void Like(object sender, RoutedEventArgs e)
        {
            string postId = "";
            DataContest dataContext = new DataContest();
            List<Post> posts = dataContext.Posts;

            List<Post> UsersPost = posts.Where(u => u.Name == username && u.Surname == usersurname).ToList(); //посортували пости за датою
            for (int i = 0; i < UsersPost.Count; i++)
                postId = UsersPost[Convert.ToInt32(likepostnumber.Text) - 1].Id;
            Post newcom = posts.Where(u => u.Id == postId).FirstOrDefault();
            newcom.Like++;
            PostService postservice = new PostService();
            postservice.Update(postId, newcom);

        }

        private void UnfollowUser(object sender, RoutedEventArgs e)
        {
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users;
            User founduser = users.Where(u => u.Id == userId).FirstOrDefault();
            User ouruser = users.Where(u => u.Id == ourId).FirstOrDefault();

            ouruser.Following.Remove(founduser.Id);
            founduser.Followers.Remove(ouruser.Id);

            UserService userservice = new UserService();
            userservice.Update(ourId, ouruser);
            userservice.Update(userId, founduser);
        }
    }
}
