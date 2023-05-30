using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.ViewModels
{
    public class ProfileDetails
    {
        [JsonProperty("wqcid")]
        public string WQCID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }    // Town == County

        [JsonProperty("address")]
        public ProfileAddress Address = new ProfileAddress();

        [JsonProperty("rateTable")]
        public List<RateTable> RateTable = new List<RateTable>();
    }

    public class FacilityDetails
    {
        [JsonProperty("wqcid")]
        public string WQCID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }    // Town == County

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("address3")]
        public string Address3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }

    public class RateTable
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; } // action name

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("notToExceed")]
        public string NotToExceed { get; set; }

        [JsonProperty("hourly")]
        public string Hourly { get; set; }
    }

    public class ProfileAddress { 
        
        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("address3")]
        public string Address3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }
}
