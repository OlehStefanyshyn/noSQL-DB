using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CassandraNetwork.Models
{
    public class UsersPathModel
    {
        public int UserFromId { get; set; }
        public int UserToId { get; set; }
        public List<UserModel> PathToUser { get; set; }
        public int PathLen { get; set; }
    }
}