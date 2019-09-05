using System.Collections.Generic;

namespace LookupApi.Models
{
    public class TalendDocument
    {
        public IEnumerable<SuccessfulItem> SuccessfulItems { get; set; }

        public IEnumerable<FailedItem> FailedItems { get; set; }
    }
}