using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bills.Models.ModelView
{
    public class BillItemView
    {
       
        public int ItemCode { get; set; }

        public int SellingPrice { get; set; }

        public int Quantity { get; set; }

        public int TotalBalance { get; set; }

        public string ItemName { get; set; }

        public string Unit { get; set; }

        public int Discount { get; set; }

 

    }
}
