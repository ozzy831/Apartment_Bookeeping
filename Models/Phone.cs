using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Phone
    {
        public int PhoneID { set; get; }

        [Display(Name="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { set; get; }

        //public int PersonID { set; get; }
        //public virtual Person Person { set; get; }
    }
}