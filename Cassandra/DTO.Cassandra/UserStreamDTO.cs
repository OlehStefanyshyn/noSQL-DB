
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Cassandra
{
    public class UserStreamDTO
    {
        public long UserID { get; set; }

        public List<CPostDTO> Stream { get; set; }
    }
}