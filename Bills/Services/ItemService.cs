using Bills.Models.Entities;
using Bills.Repository;
using Bills.Services.Interfaces;
using System.Collections.Generic;

namespace Bills.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
           _itemRepository = itemRepository;
        }
        public int create(Item item)
        {
           return _itemRepository.Add(item);
        }

        public List<Item> getAll()
        {
           return _itemRepository.GetAll();
        }

        public Item getById(int id)
        {
           return _itemRepository.GetById(id);
        }

        public List<Item> search(string search)
        {
            return _itemRepository.search(search);
        }

        public bool Unique(string Name)
        {
            Item item = _itemRepository.GetByName(Name);
            if (item != null)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
    }
}
