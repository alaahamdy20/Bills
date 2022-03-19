using Bills.Models.Entities;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface IItemService
    {

        public bool Unique(string Name);

        public List<Item> getAll();
        public int create(Item item);

        public Item getById (int id);

   
    }
}
