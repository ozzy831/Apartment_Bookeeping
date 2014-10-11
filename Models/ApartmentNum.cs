using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class ApartmentNum
    {
        public int ANID { set; get; }

        [Display(Name="Apartment Number")]
        public string AptNum { set; get; }
        
        [Display(Name="Number of Bedrooms")]
        [Range(0, 10)]
        public int NumRooms { set; get; }
        
        [Display(Name="Number of Bathrooms")]
        [Range(0,10)]
        public int NumBaths { set; get; }
        
        [Display(Name="Rent Price")]
        [DataType(DataType.Currency)]
        public decimal RentPrice { set; get; }

        public int ApartmentID { set; get; }
        public virtual Apartment Apartment { set; get; }

        
        //public int? PersonID { set; get; }
        public virtual Tenant Tenant { set; get; }
    }
}