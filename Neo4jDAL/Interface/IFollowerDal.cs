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
        void ToFollow(UserLableDTO from, UserLableDTO to);
        void ToFollow(int from, int to);
        void NewUser(UserLableDTO u);
        void DeleteUser(UserLableDTO u);
        UserLableDTO GetUserById(int id);
        void DeleteInfo(UserLableDTO u1, UserLableDTO u2);
        bool IsInfo(UserLableDTO u1, UserLableDTO u2);
        bool IsInfoC(int from, int to);
        int Path(UserLableDTO u1, UserLableDTO u2);
        int Path(int id1, int id2);
    }
}
