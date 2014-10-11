using System.ComponentModel.DataAnnotations;

namespace Apartment_Bookeeping.Models
{
    public class ExpDetail
    {
        public int ExpDetailID { set; get; }

        [Display(Name="Item Name")]
        public string Item { set; get; }

        [DataType(DataType.Currency)]
        public decimal Cost { set; get; }

        [Range(0, 100)]
        public int Quantity { set; get; }

        public int ExpenditureID { set; get; }
        public virtual Expenditure Expenditure { set; get; }
    }
}