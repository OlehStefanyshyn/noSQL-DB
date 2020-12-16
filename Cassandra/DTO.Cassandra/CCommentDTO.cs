using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace DTO.Cassandra
{
    class CCommentDTO
    {
        [TableName("comments")]
        [PrimaryKey("Id")]
        public string Id { get; set; }
        [Column("body")]
        public string CommentBody { get; set; }
        [Column("surname")]
        public string Surname { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("date")]
        public object PostCreateDate { get; set; }
    }
}
