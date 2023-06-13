using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Models
{
    public class Note
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedAt { get; set; }
    }
}
