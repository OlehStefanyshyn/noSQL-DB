using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Cassandra;

namespace DAL.Cassandra.Interfaces
{
    public interface ICPostDal
    {
        CPostDTO CreatePost(string e);
        void ReadAll();
        CPostDTO GetPost(string n, string s);
        CPostDTO CommentPost(string s, string n);
        CPostDTO LikePost(string n, string s);
        void PostOverview(string n, string s, string e);

    }
}
