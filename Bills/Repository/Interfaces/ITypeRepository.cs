using Bills.Models.Entities;

namespace Bills.Repository
{
	public interface ITypeRepository : IRepository<TypeData>
	{
		TypeData GetByName(string Name);

	}
}
