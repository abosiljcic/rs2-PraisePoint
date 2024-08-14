using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reward.API.Entities
{
    public class Points
    {
        [BsonElement("user_id")]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string user_id { get; set; }
        public int received_points { get; set; }
        public int budget { get; set; }
        public required string company_id { get; set; }
    }
}
