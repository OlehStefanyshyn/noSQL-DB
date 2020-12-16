using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Cassandra;

namespace DAL.Cassandra.Profiles
{
    class StreamProfile:Mappings
    {
        For<StreamDTO>()
                 .TableName("stream")
                 .PartitionKey(s => s.User_Id)
                 .ClusteringKey(s => s.Post_Id)
                 .CaseSensitive()
                 .Column(s => s.User_Id, cm => cm.WithName("User_Id"))
                 .Column(s => s.Post_Id, cm => cm.WithName("Post_Id"))
                 .Column(s => s.Body, cm => cm.WithName("body"))
                 .Column(s => s.Title, cm => cm.WithName("title"))
                 .Column(s => s.Likes, cm => cm.WithName("likes"))
                 .Column(s => s.Date, cm => cm.WithName("date"))
                 .Column(s => s.Comments, cm => cm.WithName("Comments"))
                 .Column(s => s.Comments, cm => cm.WithFrozenValue());
    }
}
