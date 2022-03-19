using Bills.Models.Entities;
using Bills.Models.ModelView;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface IBillService
    {
        public List<Bill> getAll();
        public int create(BillView billViewModel, BillView billViewSession);

        public BillItemView transferToBillItemView(BillView billView);
    }
}
