using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Cassandra
{
    class StreamDTO
    {
        public long User_Id { get; set; }
        public Guid Post_Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<CCommentDTO> Comments { get; set; }

    }

}
  
