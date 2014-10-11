using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class BillDetail
    {
        public int BillDetailID { set; get; }

        [Display(Name="Service Provided")]
        public string Service { set; get; }

        [DataType(DataType.Currency)]
        public decimal Cost { set; get; }

        public int BillID { set; get; }
        public virtual Bill Bill { set; get; }
    }
}