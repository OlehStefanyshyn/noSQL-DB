using Cassandra.Mapping;
using DTO.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cassandra.Profiles
{
    class CommentProfile: CustomMapping
    {

        public CommentProfile()
        {
            For<CCommentDTO>()
                  .TableName("comments")
                  .PartitionKey(c => c.Id)
                  .CaseSensitive()
                  .Column(c => c.Id, cm => cm.WithName("Id"))
                  .Column(c => c.body, cm => cm.WithName("body"))
                  .Column(c => c.surname, cm => cm.WithName("surname"))
                  .Column(c => c.name, cm => cm.WithName("name"))
                  .Column(c => c.Date, cm => cm.WithName("date"));
           }
    }
}
