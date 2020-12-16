using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic.Interfaces;
using DAL.Cassandra.Interfaces;
using DTO.Cassandra;



namespace BussinesLogic
{
    public class CassandraUser : ICassandraUser
    {
        private readonly ICUserDal _cuserDal;
        private readonly ICPostDal _cpostDal;
        public CassandraUser(ICUserDal userDal, ICPostDal postDal)
        {
            _cuserDal = userDal;
            _cpostDal = postDal;
        }

        public void Login(string email)
        {
            _cuserDal.Check(email);
        }
        public void ToFollow(string n, string s, string e)
        {
            _cuserDal.Follow(n, s, e);
        }
        public void ReadAllPosts()
        {
            _cpostDal.ReadAll();
        }
        public CPostDTO CreatePost(string e)
        {
            return _cpostDal.CreatePost(e);
        }
        public void PostReaction(string n, string s, string e)
        {
            _cpostDal.PostOverview(n, s, e);
        }
    }
}

