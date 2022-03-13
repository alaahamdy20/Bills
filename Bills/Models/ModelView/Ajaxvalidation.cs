namespace Bills.Models.ModelView
{
    public class Ajaxvalidation
    {
        public bool status { get; set; }
        public string Date { get; set; }
        public string clintError { get; set; }

        public string ItemCode { get; set; }
        public string Quantity { get; set; }   
        
       public BillView BillView { get; set; }


    }
}
