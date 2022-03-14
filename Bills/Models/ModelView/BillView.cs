using Bills.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Bills.Models.ModelView
{
    public class BillView
    {
        public BillView()
        {
            listItemView = new List<BillItemView>();
            ItemsQuantity = new Dictionary<int,int>();
        }

        #region bill data 

        [Display(Name = "Bill Number :")]
        public int BillId { get; set; }

        [Remote(action: "RequiredBillDate", controller: "Bills", ErrorMessage = "Bill Date is Required ")]
        [Display(Name = "Bill Date :")]
        public DateTime BillDate { get; set; }

        [Remote(action: "RequiredClint", controller: "Bills", ErrorMessage = "Client Name is Required")]
        [Display(Name = "Client Name :")]
        public int ClintId { get; set; }
        #endregion

        #region bill details 

        [Display(Name = "  TOTAL BILLS :")]
        [Required(ErrorMessage = "BILLS TOTAL is Required so please 'Go up and add one Item at least'")]
        [Range(1, int.MaxValue, ErrorMessage = "BILLS TOTAL is Required so please 'Go up and add at least one Item'")]
        public float? BillsTotal { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Percentage discount Must be Greater than or equal Zero")]
        public float PercentageDiscount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value discount Must be Greater than or equal Zero")]
        public float ValueDiscount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Paid Up Must be Greater than or equal Zero")]
        public float TheNet { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Paid Up Must be Greater than or equal Zero")]
        public float PaidUp { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Paid Up Must be Greater than or equal Zero")]
        public float TheRest { get; set; }

        #endregion

        #region bill item view 

        [Display(Name = "Item Name :")]
        [Remote(action: "RequiredItemId", controller: "Bills", ErrorMessage = "ITEM NAME is Required")]
        public int ItemCode { get; set; }

        [Display(Name = "Selling Price :")]
        // [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be Greater than Zero")]
        [Required(ErrorMessage = "Selling Price is Required")]
        public int SellingPrice { get; set; }

        [Remote(action: "comparisonQuantity", controller: "Bills", AdditionalFields = "ItemCode", ErrorMessage = "We don't have this quantity ")]
        [Display(Name = "Quantity :")]
        [Required(ErrorMessage = "Quantity is Required")]
        // [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be Greater than Zero")]
        public int Quantity { get; set; }

        [Display(Name = "Total :")]
        public int TotalBalance { get; set; }

        public string ItemName { get; set; }

        public string Unit { get; set; }

        public int Discount { get; set; }

        #endregion

        public Dictionary<int,int> ItemsQuantity { get; set; }

        public List<BillItemView> listItemView { get; set; }



    }
}
