using System;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class RentPayment
    {
        public int RentPaymentID { set; get; }

        [Display(Name="Receipt Number")]
        public string Receipt { set; get; }

        [Display(Name="Payment Date")]
        [DataType(DataType.Date)]
        public DateTime Date { set; get; }

        [Display(Name="Amount Paid")]
        [DataType(DataType.Currency)]
        public decimal AmountPaid { set; get; }

        public int PersonID { set; get; }
        public virtual Tenant Tenant { set; get; }
    }
}