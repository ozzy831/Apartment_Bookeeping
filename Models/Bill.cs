using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Bill
    {
        public int BillID { set; get; }

        [Display(Name="Account Number")]
        public string AccountNum { set; get; }
        
        [Display(Name="Statement Date")]
        [DataType(DataType.Date)]
        public DateTime StatementDate { set; get; }
        
        [Display(Name="Date Paid")]
        [DataType(DataType.Date)]
        public DateTime PaidDate { set; get; }
        
        [Display(Name="Amount Owed")]
        [DataType(DataType.Currency)]
        public decimal AmountOwed { set; get; }
        
        [Display(Name="Amount Paid")]
        [DataType(DataType.Currency)]
        public decimal AmountPaid { set; get; }

        public int ApartmentID { set; get; }
        public virtual Apartment Apartment { set; get; }

        public int BillingCompanyID { set; get; }
        public virtual BillingCompany BillingCompany { set; get; }

        public virtual ICollection<BillDetail> BillDetails { set; get; }
    }
}