using System.ComponentModel.DataAnnotations;

namespace Bills.Models.ModelView
{
    public class BillDetailsViews
    {
        [Display(Name = "  TOTAL BILLS :")]
        public int BillsTotal { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Percentage discount Must be Greater than or equal Zero")]
        public float PercentageDiscount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value discount Must be Greater than or equal Zero")]
        public float ValueDiscount { get; set; }

        public float TheNet { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Paid Up Must be Greater than or equal Zero")]
        public float PaidUp { get; set; }

        public float TheRest { get; set; }
    }
}
