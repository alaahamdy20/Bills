using Bills.Models.Entities;
using Bills.Models.ModelView;
using Bills.Repository;
using Bills.Services.Interfaces;
using System.Collections.Generic;

namespace Bills.Services
{
    public class BillService : IBillService
    {

        private readonly IBillRepository _billRepository;
        private readonly IItemRepository _itemRepository;
        public BillService(IBillRepository billRepository, IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _billRepository = billRepository;
        }
        public List<Bill> getAll()
        {
            return _billRepository.GetAll();
        }

        public int create(BillView billViewModel , BillView billViewSession)
        {
            #region add bill 

            #region create bill 
            Bill bill = new Bill();
            bill.BillDate = billViewSession.BillDate;
            bill.Id = billViewSession.BillId;
            bill.ClintId = billViewSession.ClintId;

            bill.BillsTotal = (float)billViewModel.BillsTotal;
            bill.PercentageDiscount = billViewModel.PercentageDiscount;
            bill.ValueDiscount = billViewModel.ValueDiscount;
            bill.PaidUp = billViewModel.PaidUp;
            bill.TheRest = billViewModel.TheRest;
            bill.TheNet = billViewModel.TheNet;
            #endregion

            #region add Items to this bill 
            foreach (BillItemView itemView in billViewSession.listItemView)
            {
                BillItem billItem = new BillItem();
                billItem.billId = bill.Id;
                billItem.ItemId = itemView.ItemCode;
                billItem.SellingPrice = itemView.SellingPrice;
                billItem.Quantity = itemView.Quantity;
                billItem.TotalBalance = itemView.TotalBalance;
                bill.BillItems.Add(billItem);
            }
            #endregion

            #region update Quantity Rest for items
            foreach (var newQuantity in billViewSession.ItemsQuantity)
            {
                Item item = _itemRepository.GetById(newQuantity.Key);
                item.QuantityRest = newQuantity.Value;
                _itemRepository.Update(item.Id, item);
            }
            #endregion

          
           return _billRepository.Add(bill);

            #endregion
        }

        public BillItemView transferToBillItemView(BillView billView)
        {
            BillItemView billItemView = new BillItemView();
            billItemView.ItemCode = billView.ItemCode;
            billItemView.SellingPrice = billView.SellingPrice;
            billItemView.Quantity = billView.Quantity;
            billItemView.TotalBalance = billView.Quantity * billView.SellingPrice;
            Item item = _itemRepository.GetById(billItemView.ItemCode);
            billItemView.ItemName = item.Name;
            billItemView.Unit = item.Unit.Name;

            return billItemView;
        }
    }
}
