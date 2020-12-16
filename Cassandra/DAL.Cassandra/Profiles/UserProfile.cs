using Cassandra.Mapping;
using DTO.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cassandra.Profiles
{
    class UserProfile: Mappings
    {
        For<CUserDTO>()
                 .TableName("users")
                 .PartitionKey(u => u.Id)
                 .CaseSensitive()
                 .Column(u => u.Id, cm => cm.WithName("Id"))
                 .Column(u => u.Name, cm => cm.WithName("name"))
                 .Column(u => u.Surname, cm => cm.WithName("surname"))
                 .Column(u => u.Interest, cm => cm.WithName("interests"))
                 .Column(u => u.Email, cm => cm.WithName("email"))
                 .Column(u => u.Password, cm => cm.WithName("password"))
                 .Column(u => u.Follow, cm => cm.WithName("users"));
    }
}
