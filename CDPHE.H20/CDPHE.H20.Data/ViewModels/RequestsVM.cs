using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.ViewModels
{
    public class RequestsVM
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("wqcid")]
        public string WQCID { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("facility")]
        public string Facility { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalCost")]
        public string TotalCost { get; set; }
    }
}
