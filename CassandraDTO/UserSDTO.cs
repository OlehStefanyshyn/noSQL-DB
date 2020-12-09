using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDTO
{
    public class UserSDTO
    {
        public long User_Id { get; set; }

        public List<PostDTO> S { get; set; }
    }
}
