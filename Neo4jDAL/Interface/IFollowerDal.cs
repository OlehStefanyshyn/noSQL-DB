using Neo4jDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jDal.Interface
{
    public interface IFollowerDal
    {
        void AddUser(UserLableDTO u);
        void DeleteUser(UserLableDTO u);
        UserLableDTO GetUserById(int id);
        void DeleteAllRelationships(UserLableDTO u1, UserLableDTO u2);
        void AddFollow(UserLableDTO from, UserLableDTO to);
        void AddFollow(int from, int to);
        bool HasAnyRelationship(UserLableDTO u1, UserLableDTO u2);
        bool HasRelationship(int from, int to);
        int MinPathBetween(UserLableDTO u1, UserLableDTO u2);
        int MinPathBetween(int id1, int id2);
    }
}
