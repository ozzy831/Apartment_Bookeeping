using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Manager : Person
    {
        public string Address { set; get; }

        [Display(Name="Zip Code")]
        public int ZipCode { set; get; }
        public virtual Location Location { set; get; }

        /*[ForeignKey("Apartments")]
        public int ApartmentID { set; get; }*/
        //public virtual Apartment Apartment { set; get; }
        public virtual ICollection<Apartment> Apartments { set; get; }
    }
}