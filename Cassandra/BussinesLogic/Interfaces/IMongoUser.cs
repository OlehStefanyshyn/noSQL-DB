using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Interfaces
{
    interface IMongoUser
    {
         void Login(string email);
         void ToFollow(string n, string s, string e);
         void ReadAllPosts();
         void CreatePost(string e);
         void PostReaction(string n, string s, string e);
           
    }
}


