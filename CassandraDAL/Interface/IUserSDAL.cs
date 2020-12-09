using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDAL.Interface
{
    public interface IUserSDAL
    {
        UserSDTO GetStreamForUser(long id, int LIMIT);

        void AddPostToUsersS(PostDTO post, List<long> UsersIds);
        void AddPostToUsersS(PostDTO post, List<string> UsersLogins);

        void UpdateSPost(PostDTO post, List<long> UsersIds);
    }
}
