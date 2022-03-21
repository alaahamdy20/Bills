using Bills.Models.Entities;
using System.Collections.Generic;

namespace Bills.Repository
{
	public interface IItemRepository : IRepository<Item>
	{
		public Item  GetByName(string Name); 
			public List<Item> search(string Name);

	}
}
