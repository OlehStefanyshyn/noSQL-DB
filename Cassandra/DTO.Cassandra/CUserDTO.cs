using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace DTO.Cassandra
{
    class CUserDTO
    {
        [TableName("users")]
        [PrimaryKey("Id")]
        public string Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("surname")]
        public string Surname { get; set; }
        [Column("interests")]
        public List<string> Interest { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("users")]
        public List<CUserDTO> Follow { get; set; }
    }
}
