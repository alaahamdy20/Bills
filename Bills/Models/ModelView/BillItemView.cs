using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bills.Models.ModelView
{
    public class BillItemView
    {
        [Display(Name = "Item Name :")]
        [Remote(action: "RequiredItemId", controller: "Bills", ErrorMessage = "ITEM NAME is Required")]
        public int ItemCode { get; set; }

        [Display(Name = "Selling Price :")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be Greater than Zero")]
        [Required(ErrorMessage = "Selling Price is Required" )]
        public int SellingPrice { get; set; }

        [Remote(action: "comparisonQuantity", controller: "Bills", AdditionalFields = "ItemCode", ErrorMessage = "We don't have this quantity ")]
        [Display(Name = "Quantity :")]
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be Greater than Zero")]
        public int Quantity { get; set; }

        [Display(Name = "Total :")]
        public int TotalBalance { get; set; }

        public string ItemName { get; set; }

        public string Unit { get; set; }

        public int Discount { get; set; }

 

    }
}
