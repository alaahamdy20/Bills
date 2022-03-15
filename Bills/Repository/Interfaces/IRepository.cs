using System.Collections.Generic;

namespace Bills.Repository
{
    public interface IRepository<T>
    {
        int Add(T newT);
        int Delete(int id);
        List<T> GetAll();
        T GetById(int id);
        int Update(int id, T newT);
    }
}
