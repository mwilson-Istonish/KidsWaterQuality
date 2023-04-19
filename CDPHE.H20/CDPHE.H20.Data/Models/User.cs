using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Models
{
    public class User
    {
        // Comment here
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int? RoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string LoginKey { get; set; }

        public DateTime? LoginKeyExpiration { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsActive { get; set; }
    }
}
