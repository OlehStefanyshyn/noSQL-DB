using AutoMapper;
using Cassandra;
using Cassandra.Mapping;
using DalCassandra.Interface;
using DalCassandra.Profiles;
using DalCassandra.Profiles.UDT;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDAL.Concrete
{
    public class UserS : UserS
    {
        private readonly Cluster _cluster;
        private readonly string _keyspace;
        private AutoMapper.IMapper _StreamMapper;
        public UserSDAL(string keyspace = "social_network", string[] nodes = null, ConsistencyLevel consistencyLevel = ConsistencyLevel.One)
        {
            nodes = new string[] { "127.0.0.1" };
            var conf = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserSProfile());
                mc.AddProfile(new PostToSProfile());
            });

            _SMapper = conf.CreateMapper();
            _keyspace = keyspace;
            _cluster= Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistencyLevel))
                .Build();
            if (MappingConfiguration.Global.Get<SProfile>() == null)
            {
                try
                {

                    MappingConfiguration.Global.Define<SProfile>();
                }
                catch(Exception exp)
                {

                }
            }
            if (MappingConfiguration.Global.Get<UserProfile>() == null)
            {
                try
                {
                    MappingConfiguration.Global.Define<UserProfile>();
                }
                catch(Exception exp)
                {

                }
            }
            if(MappingConfiguration.Global.Get<UserSProfileCassandra>() == null)
            {
                try
                {
                    MappingConfiguration.Global.Define<UserSProfileCassandra>();
                }
                catch(Exception exp)
                {

                }
            }
           
        }
        public void AddPostToUsersS(PostDTO post, List<long> UsersIds)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                SDTO S = _SMapper.Map<SDTO>(post);
                foreach(var u in UsersIds)
                {
                    S.User_Id = u;
                    mapper.Insert(S);
                }
            }
        }

        public void AddPostToUsersS(PostDTO post, List<string> UsersLogins)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                SDTO S = _SMapper.Map<SDTO>(post);

                var Ids = mapper.Fetch<UserDTO>("where \"User_Login\" in (?)",UsersLogins.ToArray()).ToList();
                this.AddPostToUsersS(post, Ids.Select(p => p.User_Id).ToList());

            }
        }

        public UserSDTO GetSForUser(long id,int LIMIT = 20)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                var user_s =mapper.Fetch<UserS>("where \"User_Id\" = ? LIMIT ? ",id,LIMIT);
                return _StreamMapper.Map<UserSDTO>(user_stream.ToList());
            }
        }

        public void UpdateSPost(PostDTO post,List<long> UsersIds)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                foreach(var user in UsersIds)
                {
                    mapper.Update<SDTO>("SET \"Likes\" =? ," +
                        "\"Dislikes\" =?," +
                        "\"Comments\"=?," +
                        "\"Modify_Date\" = ?" +
                        "where \"User_Id\" = ? and \"Post_Id\" = ? ",post.Likes,post.Dislikes,post.Comments,post.Modify_Date,user,post.Post_Id);
                }
            }
        }
    }
}
