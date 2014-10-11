using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Tenant : Person
    {
        [Display(Name = "Amount Owed")]
        [DataType(DataType.Currency)]
        public decimal AmountOwed { set; get; }

        public bool isCurrentlyTenant { set; get; }

        //zero or one-to-zero or one
        public virtual ApartmentNum ApartmentNum { set; get; }

        //one-to-zero or one
        public virtual Lawsuit Lawsuit { set; get; }

        public virtual ICollection<RentPayment> RentPayments { set; get; }
    }
}