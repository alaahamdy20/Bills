using Bills.Models.Entities;

namespace Bills.Repository
{
	public interface IItemRepository : IRepository<Item>
	{
		Item GetByName(string Name);

	}
}
