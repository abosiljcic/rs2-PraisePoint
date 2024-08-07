using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class PointsAwardedEvent : IntegrationBaseEvent
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        //public Guid CompanyId { get; set; } ovo samo u slucaju da nisu jedinstveni usernameovi (to andrijana treba da podesi)
        public int Points { get;  set; }
    }
}
