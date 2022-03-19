using Bills.Models.Entities;

namespace Bills.Repository
{
    public interface ICompanyTypeRepository : IRepository<CompanyType>
    {
        public CompanyType getByIds(int CompanyID , int TypeId);
    }
}
