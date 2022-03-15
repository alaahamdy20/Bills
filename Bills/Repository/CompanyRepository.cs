using Bills.Models;
using Bills.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Repository
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly DatabaseContext _context;

		public CompanyRepository(DatabaseContext context)
		{
			_context = context;
		}
		public int Add(CompanyData newCompany)
		{
			_context.CompanyDatas.Add(newCompany);
			return _context.SaveChanges();
		}

		public int Delete(int id)
		{
			_context.CompanyDatas.Remove(GetById(id));
			return _context.SaveChanges();
		}

		public List<CompanyData> GetAll()
		{
			return _context.CompanyDatas.ToList();
		}

		public CompanyData GetById(int id)
		{
			return _context.CompanyDatas.FirstOrDefault(c => c.Id == id);
			
		}

		public CompanyData GetByName(string Name)
		{
			return _context.CompanyDatas.Include(c => c.CompanyTypes).FirstOrDefault(c => c.Name.ToLower() == Name);

		}

		public int Update(int id, CompanyData newCompany)
		{
			
			var entry = _context.Entry(newCompany);
			entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return _context.SaveChanges();
		}
	}
}
