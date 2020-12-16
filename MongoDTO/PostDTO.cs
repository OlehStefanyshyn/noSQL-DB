using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDTO
{
    public class PostDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        [BsonElement("PostId")]
        public int PostId { get; set; }
        [BsonElement("AuthorId")]
        public int AuthorId { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Body")]
        public string Body { get; set; }

        [BsonElement("Likes")]
        public List<LikeDTO> Likes { get; set; }
        [BsonElement("Dislikes")]
        public List<DislikeDTO> Dislikes { get; set; }

        [BsonElement("Comments")]
        public List<CommentDTO> Comments { get; set; }
    }
}
