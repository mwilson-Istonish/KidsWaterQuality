using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Models
{
    public class Facility
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string WQCID { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsActive { get; set; }
    }
}
