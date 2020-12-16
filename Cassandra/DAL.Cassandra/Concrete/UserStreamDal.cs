using AutoMapper;
using Cassandra;
using Cassandra.Mapping;
using DAL.Cassandra.Profiles;
using DAL.Cassandra.Interfaces;
using DTO.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cassandra.Concrete
{
    public class UserStreamDal : IUserStreamDal
    {
        private readonly Cluster _cluster;
        private readonly string _keyspace;
        private AutoMapper.IMapper _StreamMapper;
        public UserStreamDalCassandra(string keyspace = "SocialNetwork", string[] nodes = null, ConsistencyLevel consistencyLevel = ConsistencyLevel.One)
        {
            nodes = new string[] { "127.0.0.1" };
            var conf = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserStreamProfile());
                mc.AddProfile(new PostStreamProfile());
            });

            _StreamMapper = conf.CreateMapper();
            _keyspace = keyspace;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistencyLevel))
                .Build();
            if (MappingConfiguration.Global.Get<StreamProfile>() == null)
            {
                 MappingConfiguration.Global.Define<StreamProfile>();
            }
            if (MappingConfiguration.Global.Get<UserProfile>() == null)
            {
                    MappingConfiguration.Global.Define<UserProfile>();
            }
            if (MappingConfiguration.Global.Get<UserStreamProfile>() == null)
            {
                    MappingConfiguration.Global.Define<UserStreamProfile>();
            }

        }
        public void UpdateStreamPost(CPostDTO post, List<long> UsersIds)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                foreach (var user in UsersIds)
                {
                    mapper.Update<StreamDTO>("SET \"Likes\" =? ," +
                        "\"Title\" =?," +
                        "\"Body\" =?," +
                        "\"Comments\"=?," +
                        "\"Date\" = ?" +
                        "where \"User_Id\" = ? and \"Post_Id\" = ? ", post.Like, post.Title, post.Body, post.Comments, post.PostCreateDate, user, post.Id);
                }
            }
        }

        public void AddPostToUsersStreams(CPostDTO post, List<string> UsersLogins)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                StreamDTO Stream = _StreamMapper.Map<StreamDTO>(post);

                var Ids = mapper.Fetch<CUserDTO>("where \"Email\" in (?)", UsersLogins.ToArray()).ToList();
                this.AddPostToUsersStreams(post, Ids.Select(p => p.User_Id).ToList());

            }
        }

        public UserStreamDTO GetStreamForUser(long id, int LIMIT = 20)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                var user_stream = mapper.Fetch<UserStreamDTO>("where \"User_Id\" = ? LIMIT ? ", id, LIMIT);
                return _StreamMapper.Map<UserStreamDTO>(user_stream.ToList());
            }
        }


        public void AddPostToUsersStreams(CPostDTO post, List<long> UsersIds)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                StreamDTO Stream = _StreamMapper.Map<StreamDTO>(post);
                foreach (var u in UsersIds)
                {
                    Stream.User_Id = u;
                    mapper.Insert(Stream);
                }
            }
        }
    }
}
