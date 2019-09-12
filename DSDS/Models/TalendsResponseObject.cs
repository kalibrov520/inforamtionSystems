using System;
using System.Collections.Generic;

namespace Models
{
    public class TalendResponseObject 
    {
        public int ISN { get; set; }

        public int FundId { get; set; }

        public IEnumerable<string> FundName { get; set; }

        public decimal MarketValue { get; set; }

        public decimal Shares { get; set; }

        public decimal NAV { get; set; }

        public DateTime MarketDate { get; set; }

        public DateTime ValidationDate { get; set; }
    }
}
