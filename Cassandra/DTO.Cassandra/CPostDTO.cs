using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace DTO.Cassandra
{
    public class CPostDTO
    {
        [TableName("posts")]
        [PrimaryKey("Id")]

        [PartitionKey]
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("body")]
        public string Body { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("user")]
        public CUserDTO Users { get; set; }
        [Column("tags")]
        public List<string> Tags { get; set; }
        [Column("likes")]
        public int Like { get; set; }
        [Column("comments")]
        [Cassandra.Mapping.Attributes.FrozenValue]
        public List<CCommentDTO> Comments { get; set; }
        [Column("date")]
        public object PostCreateDate { get; set; }
    }
}
