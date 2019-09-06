using System.Collections.Generic;

namespace LookupApi.Models
{
    public class TalendDocument
    {
        public IEnumerable<SuccessfulItem> Items { get; set; }
    }
}