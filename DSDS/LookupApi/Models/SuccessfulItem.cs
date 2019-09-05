using System.ComponentModel.DataAnnotations;

namespace LookupApi.Models
{
    public class SuccessfulItem
    {
        [Key]
        public int Id { get; set; }
        public int FundId { get; set; }

        public int PlanId { get; set; }

        public decimal Asset { get; set; }

        public decimal ShareBalance { get; set; }

        public decimal NAV { get; set; }

        public string PostDate { get; set; }

        public string ValuationDate { get; set; }
    }
}