using Bills.Models.Entities;
using System.Collections.Generic;

namespace Bills.Repository
{
	public interface ITypeRepository : IRepository<TypeData>
	{
		TypeData GetByName(string Name);
		public List<TypeData> getByCompanyId(int companyId);

	}
}
