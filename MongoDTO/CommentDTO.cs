using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDTO
{
    public class CommentDTO
    {
        [BsonElement("AuthorId")]
        public int AuthorId { get; set; }
        [BsonElement("CommentId")]
        public int CommentId { get; set; }
        [BsonElement("CommentText")]
        public string CommentText { get; set; }
    }
}
