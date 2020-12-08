using MongoDal.Interface;
using MongoDB.Driver;
using MongoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MongoDal.Concrete
{
    public class UserDal : IUserDal
    {
        private readonly string conct;
        private readonly string DBname;
        public UserDal(string conn,string DbName)
        {
            this.conct = conn;
            this.DBname = DbName;
        }
        public UserDTO CreateUser(UserDTO user)
        {
            try
            {
                var client = new MongoClient(conct);
                var db = client.GetDatabase(DBname);
                var users = db.GetCollection<UserDTO>("Users");

                user.UserPassword = Convert.ToBase64String(hash(user.UserPassword, "1jdkskjns"));

                var count_id = users.CountDocuments(p => p.UserId >= 0);
                user.UserId = (int)count_id + 1;

                users.InsertOne(user);
                return user;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        private byte[] hash(string password, string salt)
        {
            var alg = SHA512.Create();
            return alg.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }

        public void DeleteUser(int id)
        {
            try
            {
                var client = new MongoClient(conct);
                var db = client.GetDatabase(DBname);
                var users = db.GetCollection<UserDTO>("Users");
                users.DeleteOne(p => p.UserId == id);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                var client = new MongoClient(conct);
                var db = client.GetDatabase(DBname);
                var users = db.GetCollection<UserDTO>("Users");

                var all_users = users.Find(p => p.UserId >= 0).ToList();
                return all_users;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public UserDTO GetUserById(int id)
        {
            try
            {
                var client = new MongoClient(conct);
                var db = client.GetDatabase(DBname);
                var users = db.GetCollection<UserDTO>("Users");

                
                var founded = users.Find(p => p.UserId == id).Single();
                return founded;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public UserDTO GetUserByLogin(string login)
        {
            try
            {
                var client = new MongoClient(conct);
                var db = client.GetDatabase(DBname);
                var users = db.GetCollection<UserDTO>("Users");
                var founded = users.Find(p => p.UserLogin == login).Single();
                return founded;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public bool Login(string Login, string Password)
        {
            try
            {
                var user = this.GetUserByLogin(Login);
                if (Convert.ToBase64String(hash(Password, "1jdkskjns")) == user.UserPassword)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception exp)
            {
                throw exp;
                
            }
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            try
            {
                var client = new MongoClient(conct);
                var db = client.GetDatabase(DBname);
                var users = db.GetCollection<UserDTO>("Users");

                var UpdateFilter = Builders<UserDTO>.Update.Set("UserId", user.UserId);
                UpdateFilter = UpdateFilter.Set("UserLogin", user.UserLogin); 
                UpdateFilter = UpdateFilter.Set("UserPassword", user.UserPassword); 
                UpdateFilter = UpdateFilter.Set("UserName", user.UserName); 
                UpdateFilter = UpdateFilter.Set("UserLastName", user.UserLastName); 
                UpdateFilter = UpdateFilter.Set("FollowedIdList", user.FollowedIdList); 
                UpdateFilter = UpdateFilter.Set("Interests", user.Interests); 
                UpdateFilter = UpdateFilter.Set("Email", user.Email); 

                users.UpdateOne(g => g.UserId == user.UserId, UpdateFilter);
                var res = users.Find(p => p.UserId == user.UserId).Single();
                return res;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
