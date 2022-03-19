using Bills.Models;
using Bills.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Repository
{
    public class CompanyTypeRepository : ICompanyTypeRepository

    {
        private readonly DatabaseContext _context;
        public CompanyTypeRepository(DatabaseContext context)
        {
            _context = context;

        }
        public int Add(CompanyType newT)
        {
            _context.Add(newT);
          return _context.SaveChanges();
        }
        public CompanyType getByIds(int CompanyID, int TypeId)
        {
            return _context.CompanyTypes.Where(s => s.TypeDataId == TypeId && s.CompanyDataId == CompanyID).FirstOrDefault();
        }

        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<CompanyType> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public CompanyType GetById(int id)
        {
            throw new System.NotImplementedException();
        }



        public int Update(int id, CompanyType newT)
        {
            throw new System.NotImplementedException();
        }
    }
}
