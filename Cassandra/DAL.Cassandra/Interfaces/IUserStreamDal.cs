using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Cassandra;


namespace DAL.Cassandra.Interfaces
{
    public interface IUserStreamDal
    {
        void AddPostToUsersStreams(CPostDTO post, List<long> UsersIds);
        UserStreamDTO GetStreamForUser(long id, int LIMIT = 20);
        void AddPostToUsersStreams(CPostDTO post, List<string> UsersLogins);
        void UpdateStreamPost(CPostDTO post, List<long> UsersIds);
    }
}
