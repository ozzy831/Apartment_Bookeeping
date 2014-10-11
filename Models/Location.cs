using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Location
    {
        [Display(Name="Zip Code")]
        public int ZipCode { set; get; }

        public string City { set; get; }
        public string State { set; get; }

        public virtual ICollection<Manager> Managers { set; get; }
        public virtual ICollection<Apartment> Apartments { set; get; }
        public virtual ICollection<BillingCompany> BillingCompanies { set; get; }
    }
}