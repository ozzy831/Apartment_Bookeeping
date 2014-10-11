using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apartment_Bookeeping.Models;

namespace Apartment_Bookeeping.ViewModels
{
    public class ManagerVM
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string SSN { set; get; }
        public DateTime DOB { set; get; }
        public string PhoneNumber { set; get; }

        public string Address { set; get; }
        public int ZipCode { set; get; }

        public int AptID { set; get; }
        //public Apartment Apartment { set; get; }
    }
}