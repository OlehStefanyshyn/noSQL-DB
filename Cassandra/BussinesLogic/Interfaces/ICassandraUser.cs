using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO.Cassandra;
using System.Threading.Tasks;

namespace BussinesLogic.Interfaces
{
    interface ICassandraUser
    {
        void Login(string email);
        void ToFollow(string n, string s, string e);
        void ReadAllPosts();
        CPostDTO CreatePost(string e);
        void PostReaction(string n, string s, string e);

    }
}