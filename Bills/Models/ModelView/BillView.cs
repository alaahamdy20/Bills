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
        }

        [Display(Name = "Bill Number :")]
        public int BillId { get; set; }

        [Remote(action: "RequiredBillDate", controller: "Bills", ErrorMessage = "Bill Date is Required ")]
        [Display(Name = "Bill Date :")]
        public DateTime BillDate { get; set; }

        [Remote(action: "RequiredClint", controller: "Bills", ErrorMessage = "Client Name is Required")]
        [Display(Name = "Client Name :")]
        public int ClintId { get; set; }
        public int billsTotal { get; set; }





        public BillItemView BillItemView { get; set; }

        public List<BillItemView> listItemView { get; set; }
      
        public  BillDetailsViews BillDetails { get; set; }






    }
}
