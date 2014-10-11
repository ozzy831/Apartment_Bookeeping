using System;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Lawsuit
    {
        [Display(Name="Court Date")]
        [DataType(DataType.Date)]
        public DateTime Date { set; get; }
        
        public string Descrition { set; get; }
        public string Verdict { set; get; }

        //primary key and foreign key
        public int PersonID { set; get; }
        public virtual Tenant Tenant { set; get; }
    }
}