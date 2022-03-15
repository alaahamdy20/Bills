using Bills.Models.Entities;

namespace Bills.Repository
{
	public interface ICompanyRepository:IRepository<CompanyData>
	{
		CompanyData GetByName(string Name);

	}
}
