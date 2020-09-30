using SocialNetwork.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BuisnesLogic.Interfaces
{
    interface IUserService
    {
        User GetUser(string id);
        IEnumerable<User> GetAllUsers();
        void Create(User user);
        void Update(string id, User user);
        void Delete(string id);
    }
}
