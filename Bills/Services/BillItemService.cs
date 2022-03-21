using Bills.Models.Entities;
using Bills.Repository;
using Bills.Services.Interfaces;
using System.Collections.Generic;

namespace Bills.Services
{
    public class BillItemService : IBillItemService
    {
        private readonly IBillItemRepository _billItemRepository;

        public BillItemService(IBillItemRepository billItemRepository)
        {
            _billItemRepository= billItemRepository;
        }
        public List<BillItem> MoreDetails(int id)
        {
           return _billItemRepository.MoreDetails(id);
        }
    }
}
