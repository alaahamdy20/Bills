using Bills.Models.Entities;

namespace Bills.Repository
{
	public interface IClientRepository : IRepository<Client>
	{
		Client GetByName(string Name);

	}
}
