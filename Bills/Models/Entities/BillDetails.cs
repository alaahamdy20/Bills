using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bills.Models.Entities
{
    public class BillDetails
    {
        public int Id { get; set; }

    
        public int BillsTotal { get; set; }

  
        public int PercentageDiscount { get; set; }

       
        public int ValueDiscount { get; set; }

    
        public int TheNet { get; set; }

      
        public int PaidUp { get; set; }

        public int TheRest { get; set; }


        [ForeignKey("bill")]
        public int BillId { get; set; }
        public virtual Bill bill { get; set; }
    }
}
