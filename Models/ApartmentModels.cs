using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Bookeeping.Models
{
    public class ApartmentModels
    {
        public class Person
        {
            public int PersonID { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public string SSN { set; get; }
            public DateTime DOB { set; get; }
            public int PhoneNumber { set; get; }

            //public virtual ICollection<Phone> Phones { set; get; }
        }
        /*
        public class Phone
        {
            public int PhoneID { set; get; }
            public int PhoneNumber { set; get; }

            public int PersonID { set; get; }
            public virtual Person Person { set; get; }
        }*/

        public class Manager : Person
        {
            public string Address { set; get; }

            public int ZipCode { set; get; }
            public virtual Location Location { set; get; }

            public virtual Apartment Apartment { set; get; }
        }

        public class Location
        {
            public int ZipCode { set; get; }
            public string City { set; get; }
            public string State { set; get; }

            public virtual ICollection<Manager> Managers { set; get; }
            public virtual ICollection<Apartment> Apartments { set; get; }
            public virtual ICollection<BillingCompany> BillingCompanies { set; get; }
        }

        public class Tenant : Person
        {
            //public int TenantID { set; get; }
            public decimal AmountOwed { set; get; }
            public bool isCurrentlyTenant { set; get; }

            //zero or one-to-zero or one
            public int? ANID { set; get; }
            public virtual ApartmentNum ApartmentNum { set; get; }

            //one-to-zero or one
            public int? LawsuitID { set; get; }
            public virtual Lawsuit Lawsuit { set; get; }

            public virtual ICollection<RentPayment> RentPayments { set; get; }
        }

        public class Lawsuit
        {
            public int LawsuitID { set; get; }
            public DateTime Date { set; get; }
            public string Descrition { set; get; }
            public string Verdict { set; get; }

            public int TenantID { set; get; }
            public virtual Tenant Tenant { set; get; }
        }

        public class Apartment
        {
            public int ApartmentID { set; get; }
            public string Name { set; get; }
            public string Address { set; get; }

            public int ZipCode { set; get; }
            public virtual Location Location { set; get; }

            //one to zero or one
            //public int? PersonID { set; get; }
            public virtual Manager Manager { set; get; }

            public virtual ICollection<ApartmentNum> ApartmentNum { set; get; }

            public virtual ICollection<Bill> Bills { set; get; }
        }

        public class ApartmentNum
        {
            public int ANID { set; get; }
            public string AptNum { set; get; }
            public int NumRooms { set; get; }
            public int NumBaths { set; get; }
            public decimal RentPrice { set; get; }

            public int ApartmentID { set; get; }
            public virtual Apartment Apartment { set; get; }

            public virtual Tenant Tenant { set; get; }
        }

        public class RentPayment
        {
            public int RentPaymentID { set; get; }
            public string Receipt { set; get; }
            public DateTime Date { set; get; }
            public decimal AmountPaid { set; get; }

            public int TenantID { set; get; }
            public virtual Tenant Tenant { set; get; }
        }

        public class Bill
        {
            public int BillID { set; get; }
            public string AccountNum { set; get; }
            public DateTime StatementDate { set; get; }
            public DateTime PaidDate { set; get; }
            public decimal AmountOwed { set; get; }
            public decimal AmountPaid { set; get; }

            public int ApartmentID { set; get; }
            public virtual Apartment Apartment { set; get; }

            public int BillingCompanyID { set; get; }
            public virtual BillingCompany BillingCompany { set; get; }

            public virtual ICollection<BillDetail> BillDetails { set; get; }
        }

        public class BillingCompany
        {
            public int BillingCompanyID { set; get; }
            public string Name { set; get; }
            public string Address { set; get; }
            public int PhoneNumber { set; get; }

            public int ZipCode { set; get; }
            public virtual Location Location { set; get; }

            public virtual ICollection<Bill> Bills { set; get; }
        }

        public class BillDetail
        {
            public int BillDetailID { set; get; }
            public string Service { set; get; }
            public decimal Cost { set; get; }

            public int BillID { set; get; }
            public virtual Bill Bill { set; get; }
        }

        public class Expenditure
        {
            public int ExpenditureID { set; get; }
            public DateTime Date { set; get; }
            public decimal TotalCost { set; get; }
            public string Store { set; get; }
            public string Receipt { set; get; }

            public virtual ICollection<ExpDetail> ExpDetails { set; get; }
        }

        public class ExpDetail
        {
            public int ExpDetailID { set; get; }
            public string Item { set; get; }
            public decimal Cost { set; get; }
            public int Quantity { set; get; }

            public int ExpenditureID { set; get; }
            public virtual Expenditure Expenditure { set; get; }
        }
    }
}