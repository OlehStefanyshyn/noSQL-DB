using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDAL.Interface
{
    public interface IUserDAL
    {
        List<UserDTO> GetAllUsers();
        UserDTO GetUserByLogin(string login);
        UserDTO InsertUser(UserDTO user);
    }
}
