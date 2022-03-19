using Bills.Models.Entities;

namespace Bills.Repository
{
    public interface IUnitRepositroy:IRepository<Unit>
    {
        public Unit GetByName(string Name);
    }
}
