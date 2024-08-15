using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class NewPointsEvent : IntegrationBaseEvent
    {
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public int CompanyBudget { get; set; }
    }
}

