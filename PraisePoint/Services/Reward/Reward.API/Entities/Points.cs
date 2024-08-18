using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reward.API.Entities
{
    public class Points
    {
        [BsonElement("UserName")]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserName { get; set; }
        public int ReceivedPoints { get; set; }
        public int Budget { get; set; }
        public required string CompanyId { get; set; }
    }
}
