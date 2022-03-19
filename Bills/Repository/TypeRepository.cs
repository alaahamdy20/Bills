using Bills.Models;
using Bills.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Repository
{
	public class TypeRepository : ITypeRepository
	{
		private readonly DatabaseContext _context;

		public TypeRepository(DatabaseContext context)
		{
			_context = context;
		}
		public int Add(TypeData newType)
		{
			_context.TypeDatas.Add(newType);
			return _context.SaveChanges();
		}

		public int Delete(int id)
		{
			_context.TypeDatas.Remove(GetById(id));
			return _context.SaveChanges();
		}

		public List<TypeData> GetAll()
		{
			return _context.TypeDatas.ToList();
		}

        public List<TypeData> getByCompanyId(int companyId)
        {
            return _context.CompanyTypes.Where(s => s.CompanyDataId == companyId).Select(s => s.TypeData).ToList();
		}

        public TypeData GetById(int id)
		{
			return _context.TypeDatas.FirstOrDefault(c => c.Id == id);
			
		}

		public TypeData GetByName(string Name)
		{
			return _context.TypeDatas.Include(t=>t.CompanyTypes).FirstOrDefault(c => c.Name.ToLower() == Name);
		}



		public int Update(int id, TypeData newType)
		{
			
			var entry = _context.Entry(newType);
			entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return _context.SaveChanges();
		}
	}
}
