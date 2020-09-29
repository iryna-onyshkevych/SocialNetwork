using SocialNetwork.DataAccess.Contest;
using SocialNetwork.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContest dataContext = new DataContest();
            List<User> users = dataContext.Users; 
           User FUser = users.Where(u => u.Name == Nae && u.Pass) ;
            
            Console.ReadLine();
            
        }
    }
}
