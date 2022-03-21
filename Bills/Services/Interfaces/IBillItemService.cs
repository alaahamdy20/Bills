using Bills.Models.Entities;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface IBillItemService
    {
        List<BillItem> MoreDetails(int id);
    }
}
