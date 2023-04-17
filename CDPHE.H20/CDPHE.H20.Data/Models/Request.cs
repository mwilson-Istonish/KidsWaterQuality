using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Models
{
    public class Request
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FacilityId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsActive { get; set; }
    }
}
