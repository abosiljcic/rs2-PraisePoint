namespace Reward.API.Entities
{
    public class Points
    {
        public string user_id { get; set; }
        public int received_points { get; set; }
        public int budget { get; set; }
        public string company_id { get; set; }
    }
}
