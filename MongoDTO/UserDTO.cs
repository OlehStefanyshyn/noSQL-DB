using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDTO
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("UserLName")]
        public string UserLastName { get; set; }
        [BsonElement("UserLogin")]
        public string UserLogin { get; set; }
        [BsonElement("UserPassword")]
        public string UserPassword { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("FollowedIdList")]
        public List<int> FollowedIdList { get; set; }
    }
}
