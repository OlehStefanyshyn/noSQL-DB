using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using DalCassandra;
using DalCassandra.Concrete;
using DTOCassandra;


namespace ConsoleForTests
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var all = cDal.GetAllPosts();
            all.First();
            var added_post = cDal.AddPost(new PostDTO { 
                Author_Id=1,
                Body= "New Post",
                Title ="Title",
                Create_Date= DateTime.Now,
                Modify_Date = DateTime.Now,
                Dislikes=new List<long>() { 1 },
                Likes=new List<long>() { 2 },
                Comments = new List<Comment>() { new Comment() { Body="NEW COMMENT",User_Id=1,Create_Date=DateTimeOffset.Now,Modify_Date= DateTimeOffset.Now } }

            });

            all = cDal.GetAllPosts();
            all.First();
            
            var to_update = cDal.GetAllPosts()[1];
            Console.WriteLine("Before updated: {0}", to_update.Body);
            to_update.Body = "Changed body to check Update method";
            var updated =cDal.UpdatePost(to_update);
            Console.WriteLine("Updated: {0}",updated.Body);
            Console.ReadLine();
            */
            /*
            var added_post = cDal.AddPost(new PostDTO
            {
                Author_Id = 1,
                Body = "New Post",
                Title = "Title",
                Create_Date = DateTime.Now,
                Modify_Date = DateTime.Now,
                Dislikes = new List<long>() { 1 },
                Likes = new List<long>() { 2 },
                Comments = new List<Comment>() { new Comment() { Body = "NEW COMMENT", User_Id = 1, Create_Date = DateTimeOffset.Now, Modify_Date = DateTimeOffset.Now } }

            });

            var posts = cDal.GetAllPosts();
            var SDal = new UserSDALCassandra(keyspace: "social_network", nodes: nodes);
            
            foreach(var p in posts)
            {
                SDal.AddPostToUsersS(p, new List<long>() { 1 });
            }
            */
            //streamDal.GetSForUser(1);
            //streamDal.AddPostToUsersS(posts[0], new List<string>() {"Login"});
            
            //var uDal = new UserDALCassandra(keyspace: "social_network", nodes: nodes);
            //uDal.GetAllUsers();

        }
    }
}
