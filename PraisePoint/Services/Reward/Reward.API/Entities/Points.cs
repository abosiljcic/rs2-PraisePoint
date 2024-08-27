using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reward.API.Entities
{
    public class Points
    {
        [BsonElement("UserName")]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public required string UserName { get; set; }
        public int ReceivedPoints { get; set; }
        public int Budget { get; set; }
        public required string CompanyId { get; set; }
    }
}
