namespace Posts.Domain.Entities
{
    public class AwardPoints
    {
        public string SenderUsername { get; private set; }
        public string ReceiverUsername { get; private set; }
        //public Guid CompanyId { get; private set; }
        public int Points { get; private set; }

        public AwardPoints(string senderUsername, string receiverUsername, int points)
        {
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            Points = points;
        }
    }
}
