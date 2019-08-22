using System;

namespace CrudService.Models
{
    public class ReformattedDocument
    {
        public int Id { get; set; }

        public string BusinessEntityName { get; set; }

        public int Isn { get; set; }

        public string FundId { get; set; }

        public string FundName { get; set; }

        public decimal MarketValue { get; set; }

        public decimal Shares { get; set; }

        public decimal Nav { get; set; }

        public DateTime MarketDate { get; set; }

        public DateTime ValidationDate { get; set; }
    }
}