using System;
using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class Person
    {
        public int PersonID { set; get; }

        [Display(Name="First Name")]
        public string FirstName { set; get; }

        [Display(Name="Last Name")]
        public string LastName { set; get; }

        [Display(Name="Social Security Number")]
        [MinLength(9), MaxLength(9)]
        public string SSN { set; get; }
        
        [Display(Name="Date of Birth")]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { set; get; }
        
        [Display(Name="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter phone number as 1234567890")]
        public string PhoneNumber { set; get; }

        public string FullName { get { return FirstName + " " + LastName; } }

        //public virtual ICollection<Phone> Phones { set; get; }
    }
}