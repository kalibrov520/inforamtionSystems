using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TalendService.Models
{
    public class Document
    {
        public IEnumerable<Item> Items { get; set; }
    }

    public class Item
    {
        public int FundId { get; set; }

        public int PlanId { get; set; }

        public decimal Asset { get; set; }

        public decimal ShareBalance { get; set; }

        public decimal NAV { get; set; }

        public string PostDate { get; set; }

        public string ValuationDate { get; set; }
    }
}