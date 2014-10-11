using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Expenditure
    {
        public int ExpenditureID { set; get; }

        [Display(Name="Purchase Date")]
        [DataType(DataType.Date)]
        public DateTime Date { set; get; }
        
        [Display(Name="Total Cost")]
        [DataType(DataType.Currency)]
        public decimal TotalCost { set; get; }
        
        [Display(Name="Store Name")]
        public string Store { set; get; }
        
        [Display(Name="Receipt Number")]
        public string Receipt { set; get; }

        public virtual ICollection<ExpDetail> ExpDetails { set; get; }
    }
}