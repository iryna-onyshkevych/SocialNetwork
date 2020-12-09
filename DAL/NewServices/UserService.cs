using DAL.Enteties;
using DAL.Repository;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using MongoDB.Bson;

using DAL.Neo4JRepository;

namespace DAL.Services
{



    public class UserService
    {
        UserRepository repository;
        Graph graphRepository;
        public UserService()
        {
            repository = new UserRepository();
            graphRepository = new Graph();
        }
        //
        public bool CheckPassword(string userlog, string password)
        {

            User user = new User();
            user = repository.GetUser(userlog);
            if (user != null)
            {
                if (user.Password == GetHashStringSHA256(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public string GetHashStringSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
            string result = "";
            foreach (byte b in hashPassword)
            {
                result += b.ToString();
            }
            return result;
        }

        public bool CheckIndentityOfUserlog(string userlog)
        {
            List<User> users = new List<User>();
            users = repository.GetUsers();
            foreach (var elem in users)
            {
                if (elem.Userlog == userlog)
                {
                    return false;
                }
            }

            return true;
        }
      
        public void UserlogWrite(string Userlog)
        {
            var p = new UserLog();

            p.Userlog = Userlog;
            if (p != null)
            {
                using (FileStream fs = new FileStream("UserLog.json", FileMode.Create))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(UserLog));
                    jsonFormatter.WriteObject(fs, p);
                }
            }
            else
            {
                using (FileStream fs = new FileStream("UserLog.json", FileMode.Create))
                {
                    p = new UserLog { Userlog = "" };
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(UserLog));
                    jsonFormatter.WriteObject(fs, p);
                }
            }


        }

        public string UserlogRead()
        {
            var p = new UserLog();
            using (FileStream fs = new FileStream("UserLog.json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(UserLog));
                if (fs.Length != 0)
                {
                    p = (UserLog)jsonFormatter.ReadObject(fs);
                }

            }

            return p.Userlog;
        }
        //
        public bool CheckAlreadyFollow(string userlog, string userslog)
        {
            User user = new User();
            user = repository.GetUser(userlog);
            if (user != null && user.Following != null)
            {
                foreach (var el in user.Following)
                {
                    if (el == userslog)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool CheckIsUserInDatabase(string userlog)
        {
            User user = new User();
            user = repository.GetUser(userlog);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
       

        public bool UpdateField(string userlog, string fieldToEdit, string fieldValue)
        {
            try
            {
                repository.UpdateField(userlog, fieldToEdit, fieldValue);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void UpdateDate()
        {
            try
            {
                repository.UpdateDate(UserlogRead());
            }
            catch
            {

            }

        }
        //
        public void InsertUser(string usName, string usSurname, string usEmail, string usUserlog, string usPassword)
        {
            User user = new User();
            user.Name = usName;
            user.Surname = usSurname;
            user.Email = usEmail;
            user.Userlog = usUserlog;
            user.Password = GetHashStringSHA256(usPassword);
            DateTime date1 = new DateTime();
            user.Date = date1.ToString();
            repository.Add(user);

            graphRepository.CreatePerson(new Person()
            {
                Surname = usSurname,
                Name = usName,
                Email = usEmail,
                Userlog = usUserlog
            });
        }
        //
        public User GetUser()
        {
            try
            {
                return repository.GetUser(UserlogRead());
            }
            catch
            {
                return new User();
            }

        }

        public ObjectId GetUserId()
        {
            try
            {
                return repository.GetUserId(UserlogRead());
            }
            catch
            {
                return new ObjectId();
            }

        }

        public User GetUser(string userlog)
        {
            try
            {
                return repository.GetUser(userlog);
            }
            catch
            {
                return new User();
            }

        }

        public User GetUser(ObjectId id)
        {
            try
            {
                return repository.GetUser(id);
            }
            catch
            {
                return new User();
            }

        }
        //
        public List<string> GetFollowers()
        {
            List<string> ls = new List<string>();
            try
            {
                ls = repository.GetFollowers(UserlogRead());
                return ls;
            }
            catch
            {
                return ls;
            }
        }

        public List<string> GetFollowing()
        {
            List<string> ls = new List<string>();
            try
            {
                ls = repository.GetFollowing(UserlogRead());
                return ls;
            }
            catch
            {
                return ls;
            }
        }
        //

        public List<Person> GetFriendsOfFriend()
        {
            List<Person> res = new List<Person>();
            User user = new User();

            user = GetUser(UserlogRead());

            var people = graphRepository.FriendsOfAFriend(new Person()
            {

                Surname = user.Surname,
                Name = user.Name,
                Email = user.Email,
                Userlog = user.Userlog
            });

            foreach (var elem in people)
            {
                bool temp = true;
                foreach (var el in res)
                {
                    if (el.Userlog == elem.Userlog)
                    {
                        temp = false;
                    }
                }
                if (temp)
                {
                    res.Add(elem);
                }
            }

            return res;

        }

        public List<string> GetConnectingPaths(string userlog)
        {
            try
            {
                List<string> res = new List<string>();

                User user1 = new User();
                user1 = GetUser(UserlogRead());

                User user2 = new User();
                user2 = GetUser(userlog);

                var temp = graphRepository.ConnectingPaths(new Person()
                {
                    Surname = user1.Surname,
                    Name = user1.Name,
                    Userlog = user1.Userlog,
                    Email = user1.Email
                },
                new Person()
                {
                    Surname = user2.Surname,
                    Name = user2.Name,
                    Userlog = user2.Userlog,
                    Email = user2.Email
                });

                foreach (var elem in temp)
                {
                    res.Add(elem);
                }

                return res;
            }
            catch
            {
                return new List<string>();
            }
        }

        public string GetConnectingPathsNumber(string userlog)
        {
            try
            {
                List<string> res = new List<string>();

                User user1 = new User();
                user1 = GetUser(UserlogRead());

                User user2 = new User();
                user2 = GetUser(userlog);

                var temp = graphRepository.ConnectingPaths(new Person()
                {
                    Surname = user1.Surname,
                    Name = user1.Name,
                    Userlog = user1.Userlog,
                    Email = user1.Email
                },
                new Person()
                {
                    Surname = user2.Surname,
                    Name = user2.Name,
                    Userlog = user2.Userlog,
                    Email = user2.Email
                });

                foreach (var elem in temp)
                {
                    res.Add(elem);
                }
                if (res.Count == 0)
                {
                    return "No connection";
                }
                else if (res.Count == 2)
                {
                    return "following";
                }
                else if (res.Count - 1 > 1)
                {
                    return "Connection : " + (res.Count - 1).ToString();
                }
                else
                {
                    return "No connection";
                }
            }
            catch (Exception e)
            {
                return " ";
            }
        }

        public void AddFollower(string userlog, string newFollower)
        {
            repository.addFollower(userlog, newFollower);
        }

        public void UnFollow(string userlog, string follower)
        {
            repository.unFollow(userlog, follower);
            User user1 = new User();
            user1 = GetUser(userlog);

            User user2 = new User();
            user2 = GetUser(follower);

            graphRepository.DeleteRelationShip(new Person()
            {
                Surname = user1.Surname,
                Name = user1.Name,
                Email = user1.Email,
                Userlog = user1.Userlog
            }, new Person()
            {
                Surname = user2.Surname,
                Name = user2.Name,
                Userlog = user2.Userlog,
                Email = user2.Email
            });
        }

        public void AddFollowing(string userlog, string newFollowing)
        {
            repository.addFollowing(userlog, newFollowing);

            User user1 = new User();
            user1 = GetUser(userlog);

            User user2 = new User();
            user2 = GetUser(newFollowing);

            graphRepository.CreatRelationShip(new Person()
            {
                Surname = user1.Surname,
                Name = user1.Name,
                Email = user1.Email,
                Userlog = user1.Userlog
            }, new Person()
            {
                Surname = user2.Surname,
                Name = user2.Name,
                Userlog = user2.Userlog,
                Email = user2.Email
            });
        }

      
    }
}
