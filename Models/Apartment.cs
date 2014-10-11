using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Apartment
    {
        public int ApartmentID { set; get; }

        [Display(Name="Apartment Name")]
        public string Name { set; get; }
        public string Address { set; get; }

        [Display(Name="Zip Code")]
        public int ZipCode { set; get; }
        public virtual Location Location { set; get; }

        //[ForeignKey("Manager")]
        public int? PersonID { set; get; }
        public virtual Manager Manager { set; get; }

        public virtual ICollection<ApartmentNum> ApartmentNums { set; get; }

        public virtual ICollection<Bill> Bills { set; get; }
    }
}