using System;

namespace CrudService.Models
{
    public class Document
    {
        public string BusinessEntityName { get; set; }
        public string ISN { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public float MarketValue { get; set; }
        public float Shares { get; set; }
        public float NAV { get; set; }
        public DateTime MarketDate { get; set; }
        public DateTime ValidationDate { get; set; }
    }

}