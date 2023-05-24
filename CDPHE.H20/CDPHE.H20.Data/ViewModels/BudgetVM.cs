using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.ViewModels
{
    public class BudgetVM
    {
        [JsonProperty("annual")]
        public decimal Annual { get; set; }

        [JsonProperty("spent")]
        public decimal Spent { get; set; }

        [JsonProperty("approved")]
        public decimal Approved { get; set; }

        [JsonProperty("requested")]
        public decimal Requested { get; set; }

        [JsonProperty("remaining")]
        public decimal Remaining { get; set; }
    }
}
