using System;
using Cassandra;
using Cassandra.Mapping;
using DAL.Cassandra.Interfaces;
using DAL.Cassandra.Profiles;
using DTO.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Cassandra.Concrete
{
    class CUserDal:ICUserDal
    {
       
            private readonly Cluster _cluster;
            private readonly string _keyspace;
        public CUserDal(string keyspace = "SocialNetwork", string[] nodes = null, ConsistencyLevel consistencyLevel = ConsistencyLevel.One)
        {
            nodes = new string[] { "127.0.0.1" };
            _keyspace = keyspace;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistencyLevel))
                .Build();
            if (MappingConfiguration.Global.Get<UserProfile>() == null)
            {
                MappingConfiguration.Global.Define<UserProfile>();
            }
        }



            public  void Check(string check)
            {
                bool temp = false;

                using (ISession session = _cluster.Connect(_keyspace))
                {
                    IMapper mapper = new Mapper(session);
                    session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                    var user = mapper.FirstOrDefault<CUserDTO>("where \"Email\" = ? ", check);
                    if (user != null)
                    { temp = true; }
                }


            if (temp)
            {
                    Console.WriteLine("Okay, honey, now enter your password");
                    var pas = Console.ReadLine();
                    using (ISession session = _cluster.Connect(_keyspace))
                    {
                        var user1 = mapper.FirstOrDefault<CUserDTO>("where \"Email\" = ? +\"Password\" = ?", check, pas);
                        if (user1 != null)
                        { temp = true;
                            Console.WriteLine("Well done!");
                            Console.WriteLine("Here there are recent posts:");
                            CPostDal sh = new CPostDal();
                            sh.ReadAll();
                            break;

                        }
                        else
                        {
                            temp = false;
                        }
                    }
            }
                    if (!temp)
                    {
                        //throw new ArgumentException("Unfortunutely,kitty,this password is incorrect. Please, try one more time.");
                        Console.WriteLine("Unfortunutely,kitty,this password is incorrect. Please, try one more time.");
                        Thread.Sleep(2000);
                        System.Environment.Exit(20);
                    }
                

                else
                {
                    Console.WriteLine("Unfortunutely,kitty, there isn't account with login like that. Please, try one more time.");
                    Thread.Sleep(2000);
                    //throw new ArgumentException("Unfortunutely,kitty, there isn't account with login like that. Please, try one more time.");
                    System.Environment.Exit(20);

                }
            }
        public void Follow(string n, string s, string e)
        {
            var temp = false;
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var user = mapper.FirstOrDefault<CUserDTO>("where \"Surname\" = ? + \"Name\" = ? ", s, n);


                if (user != null)
                {
                    temp = true;
                    Console.WriteLine("Found! Do you want to follow" + n + " " + s + "? Yes/No");
                    string t = Console.ReadLine();
                    if (t == "Yes")
                    {
                        var user2 = mapper.FirstOrDefault<CUserDTO>("where \"Email\" = ? ", e);
                        user2.Follow.Add(user);
                        mapper.Update<CUserDTO>(user);
                       mapper.SingleOrDefault<CUserDTO>("where \"Email\" = ?", e);
                        Console.WriteLine("Well Done!");

                    }

                }
            }

           
            if (!temp)
            {
                Console.WriteLine("There isn't such person.Don't be disappointed");
            }
       }
        
   }      
    
}
