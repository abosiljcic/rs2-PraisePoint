using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Domain.Entities
{
    public class PointsAwarded
    {
        public string SenderUsername { get; private set; }
        public string ReceiverUsername { get; private set; }
        //public Guid CompanyId { get; private set; }
        public int Points { get; private set; }

        public PointsAwarded(string senderUsername, string receiverUsername, int points)
        {
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            Points = points;
        }
    }
}
