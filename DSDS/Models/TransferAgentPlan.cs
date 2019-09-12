using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TransferAgentPlan
    {

        public int TransferAgentPlanId { get; set; }

        public int TransferAgentId { get; set; }

        public int PlanId { get; set; }

        public string AccountNumber { get; set; }

        public string OtherTransferAgentPlanInfo { get; set; }

        public bool IsActive { get; set; }
    }
}
