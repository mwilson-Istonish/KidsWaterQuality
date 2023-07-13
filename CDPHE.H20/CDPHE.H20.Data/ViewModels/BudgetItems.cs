using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.ViewModels
{
    public class BudgetItems
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal ActualMaterialCost { get; set; }
        public decimal ActualLaborCost { get; set; }
    }
}
