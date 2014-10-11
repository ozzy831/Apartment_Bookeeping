using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class BillingCompany
    {
        public int BillingCompanyID { set; get; }

        [Display(Name="Company Name")]
        public string Name { set; get; }
        
        public string Address { set; get; }
        
        [Display(Name="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { set; get; }

        [Display(Name="Zip Code")]
        public int ZipCode { set; get; }
        public virtual Location Location { set; get; }

        public virtual ICollection<Bill> Bills { set; get; }
    }
}