using Neo4jClient;
using Neo4jDal.Interface;
using Neo4jDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jDal.Concrete
{
    public class FollowerDal: IFollowerDal
    {
        private readonly string conct;
        private readonly string logg;
        private readonly string pasw;
        public FollowerDal(string conn,string login,string pass)
        {
            this.conct = conn;
            this.logg = login;
            this.pasw = pass;
        }

        public void ToFollow(UserLableDTO from, UserLableDTO to)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.UserId = {from}")
                    .AndWhere("user2.UserId = {to}")
                    .WithParam("from", from.UserId)
                    .WithParam("to", to.UserId)
                    .Create("(user1)-[:Follower]->(user2)")
                    .ExecuteWithoutResults();
            }
        }

        public void ToFollow(int from, int to)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.UserId = {from}")
                    .AndWhere("user2.UserId = {to}")
                    .WithParam("from", from)
                    .WithParam("to", to)
                    .Create("(user1)-[:Follower]->(user2)")
                    .ExecuteWithoutResults();
            }
        }

        public void NewUser(UserLableDTO u)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();

                client.Cypher.Create("(u:User { UserId: {p1},UserLogin: {p2}})")
                    .WithParam("p1", u.UserId)
                    .WithParam("p2", u.UserLogin)
                    .ExecuteWithoutResults();
            }
        }

        public void DeleteInfo(UserLableDTO u1, UserLableDTO u2)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Follower]-(user2:User)")
                    .Where("user1.UserId = {p1}")
                    .AndWhere("user2.UserId = {p2}")
                    .WithParam("p1", u1.UserId)
                    .WithParam("p2", u2.UserId)
                    .Delete("r")
                    .ExecuteWithoutResults();

            }
        }

        public void DeleteUser(UserLableDTO u)
        {
            using (var client = new GraphClient(new Uri(conct),logg, pasw))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Follower]-()")
                    .Where("user1.UserId = {p_id}")
                    .WithParam("p_id", u.UserId)
                    .Delete("r,user1")
                    .ExecuteWithoutResults();

            }
        }

        public UserLableDTO GetUserById(int id)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();
                var user = client.Cypher
                    .Match("(u:User)")
                    .Where((UserLableDTO u) => u.UserId == id)
                    .Return(u => u.As<UserLableDTO>())
                    .Results;
                UserLableDTO to_ret = new UserLableDTO() { UserId = id };
                foreach (var u in user)
                {
                    to_ret.UserId = u.UserId;
                    to_ret.UserLogin = u.UserLogin;
                }
                return to_ret;
            }
        }

        public bool IsInfo(UserLableDTO u1, UserLableDTO u2)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();
                var is_friends = client.Cypher
                   .Match("(user1:User)-[r:Follower]-(user2:User)")
                   .Where((UserLableDTO user1) => user1.UserId == u1.UserId)
                   .OrWhere((UserLableDTO user2) => user2.UserId == u2.UserId)
                   .Return(r => r.As<FollowerDTO>()).Results;
                if (is_friends.Count() > 0)
                {
                    return true;
                }
                return false;

            }
        }

        public bool IsInfoC(int from, int to)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();
                var is_friends = client.Cypher
                   .Match("(user1:User)-[r:Follower]->(user2:User)")
                   .Where((UserLableDTO user1) => user1.UserId == from)
                   .AndWhere((UserLableDTO user2) => user2.UserId == to)
                   .Return(r => r.As<FollowerDTO>()).Results;
                if (is_friends.Count() > 0)
                {
                    return true;
                }
                return false;

            }
        }

        public int Path(UserLableDTO u1, UserLableDTO u2)
        {
            return this.Path(u1.UserId, u2.UserId);
        }

        public int Path(int id1, int id2)
        {
            using (var client = new GraphClient(new Uri(conct), logg, pasw))
            {
                client.Connect();
                var res = client.Cypher
                    .Match("(u1:User{UserId: {p_id1} }),(u2:User{UserId: {p_id2} })," +
                    " p = shortestPath((u1)-[:Follower*]-(u2))")
                    .WithParam("p_id1", id1)
                    .WithParam("p_id2", id2)
                    .Return(p => p.As<FollowerPathDTO>())
                    .Results;
                int path_len = -1;
                foreach (var t in res)
                {
                    path_len = Convert.ToInt32(t.len);
                }
                return path_len;
            }
        }
    }
}
