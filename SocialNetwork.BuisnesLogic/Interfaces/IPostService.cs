using SocialNetwork.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BuisnesLogic.Interfaces
{
    public interface IPostsService
    {
            Post GetPosts(string id);
            List<Post> GetAllPosts();
            void Create(Post post);
            void Update(string id, Post post);
            void Delete(string id);

        
    }
}

