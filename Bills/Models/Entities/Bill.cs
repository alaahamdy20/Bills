using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bills.Models.Entities
{
    public class Bill
    {

        public Bill() {
            BillItems = new List<BillItem>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
      
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        [ForeignKey("client")]
        public int ClintId { get; set; }
        public virtual Client client { get; set; }

        // bill details 
        public float BillsTotal { get; set; }


        public float PercentageDiscount { get; set; }


        public float ValueDiscount { get; set; }


        public float TheNet { get; set; }

        public float PaidUp { get; set; }

        public float TheRest { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }


    }
}
