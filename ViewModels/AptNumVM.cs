using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apartment_Bookeeping.ViewModels
{
    public class AptNumVM
    {
        public int ANID { set; get; }
        public string Name { set; get; }
        public string AptNum { set; get; }
        public int NumRooms { set; get; }
        public int NumBaths { set; get; }
        public decimal RentPrice { set; get; }
        public int ApartmentID { set; get; }

        public string AptNumOld { set; get; }
    }
}