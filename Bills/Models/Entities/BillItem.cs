using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bills.Models.Entities
{
    public class BillItem
    {
        public int Id { get; set; }

        public int SellingPrice { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("bill")]
        public int billId { get; set; }
        public virtual Bill bill { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int TotalBalance { get; set; }

        








    }
}
