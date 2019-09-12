using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class TalendResponseObject 
    {
        [Key]
        public int Id { get; set; }

        public int ISN { get; set; }

        public int FundId { get; set; }

        //TODO: FIX!
        [NotMapped]
        public IEnumerable<string> FundName { get; set; }

        public decimal MarketValue { get; set; }

        public decimal Shares { get; set; }

        public decimal NAV { get; set; }

        public DateTime MarketDate { get; set; }

        public DateTime ValidationDate { get; set; }
    }
}
