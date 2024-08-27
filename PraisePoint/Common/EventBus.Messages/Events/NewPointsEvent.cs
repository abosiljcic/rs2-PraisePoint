using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class NewPointsEvent : IntegrationBaseEvent
    {
        public int CompanyBudget { get; set; }
        public Guid CompanyId { get; set; }
        public string UserName { get; set; }
    }
}

