using System;
using System.ComponentModel.DataAnnotations;

namespace TalendService.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
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