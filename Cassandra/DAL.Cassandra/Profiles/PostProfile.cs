using Cassandra.Mapping;
using DTO.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cassandra.Profiles
{
    class PostProfile:Mappings
    {
        For<CPostDTO>()
                  .TableName("posts")
                  .PartitionKey(p => p.Id)
                  .CaseSensitive()
                  .Column(p => p.Id, cm => cm.WithName("Id"))
                  .Column(p => p.body, cm => cm.WithName("body"))
                  .Column(p => p.title, cm => cm.WithName("title"))
                  .Column(p => p.Users, cm => cm.WithName("user"))
                  .Column(p => p.Like, cm => cm.WithName("likes"))
                  .Column(p => p.Tags, cm => cm.WithName("tags"))
                  .Column(p => p.Date, cm => cm.WithName("date"))
                  .Column(p => p.Comments, cm => cm.WithName("comments"))
                  .Column(p => p.Comments, cm => cm.WithFrozenValue());
    }
}
