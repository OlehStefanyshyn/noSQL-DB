using DAL.Cassandra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using DTO.Cassandra;
using Cassandra.Mapping;
using DAL.Cassandra.Profiles;

namespace DAL.Cassandra.Concrete
{
    public class CPostDal : ICPostDal
    {
        private readonly Cluster _cluster;
        private readonly string _keyspace;


        public CPostDal(string keyspace = "SocialNetwork", string[] nodes = null, ConsistencyLevel consistencyLevel = ConsistencyLevel.One)
        {
            nodes = new string[] { "127.0.0.1" };
            _keyspace = keyspace;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistencyLevel))
                .Build();
            if (MappingConfiguration.Global.Get<PostProfile>() == null)
            {
                    MappingConfiguration.Global.Define<PostProfile>();
            }

        }




        public CPostDTO CreatePost(string e)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                var TimeUUId = TimeUuid.NewId(DateTime.Now);
                CPostDTO post = new CPostDTO();
                post.Id = TimeUUId;
                Console.WriteLine("Write your post");
                string x = Console.ReadLine();
                Console.WriteLine("Write title of your post");
                string t = Console.ReadLine();
                post.Title = t;
                post.Body = x;
                mapper.Insert(post);
                return mapper.Single<CPostDTO>("where \"Post_Id\" = ? ", TimeUUId);
            }
        }



        public void ReadAll()
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var posts = mapper.Fetch<CPostDTO>("select * from posts");
                foreach (var p in posts)
                {
                    Console.WriteLine(p.Title + ": " + p.Body + " " + p.PostCreateDate + "\n  \n");
                }
            }
        }


        public CPostDTO GetPost(string n, string s)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var post = mapper.FirstOrDefault<CPostDTO>("where \"Name\" = ? + \"Surname\" = ?", n,s);
                return post;
            }
        }

        public CPostDTO CommentPost(string s, string n)
        {
            var post = this.GetPost(n, s);
            CCommentDTO comment = new CCommentDTO();
            Console.WriteLine(" Write you comment:");
            var y = Console.ReadLine();
            comment.CommentBody = y;
            comment.Surname = s;
            comment.Name = n;
            comment.PostCreateDate = DateTime.Now;
            post.Comments = comment;

            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                mapper.Update<CPostDTO>(post);
                return mapper.SingleOrDefault<CPostDTO>("where \"Post_Id\" = ?", post.Id);

            }

        }

        public CPostDTO LikePost(string n, string s)
        {
            var post = this.GetPost(n,s);
            
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                post.Like = post.Like + 1;
                mapper.Update<CPostDTO>(post);
                return mapper.SingleOrDefault<CPostDTO>("where \"Post_Id\" = ?", post.Id);


            }
        }


        public void PostOverview(string n,string s,string e)
        {
            Console.WriteLine("Do you want to react on posts?\n 1-yes\n 2-no ");
            var x = Console.ReadLine();
            switch (x)
            {
                case "1":
                    Console.WriteLine(" 1-like\n 2-comment");
                    var z = Console.ReadLine();
                    switch (z)
                    {
                        case "1":
                            LikePost(n,s);
                            break;
                        case "2":
                            CommentPost(n, s);
                            Console.WriteLine("Well Done!");

                            break;
                    }
                    break;
                case "2":
                    // Program.Menu(e);
                    break;
            }
        }
    }
}