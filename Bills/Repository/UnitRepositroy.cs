using Bills.Models;
using Bills.Models.Entities;
using System.Collections.Generic;
using System.Linq;
namespace Bills.Repository
{
    public class UnitRepositroy:IUnitRepositroy
    {
        public readonly DatabaseContext _context;
        public UnitRepositroy(DatabaseContext context)
        {
            _context = context;
        }

        public int Add(Unit newunit)
        {
            _context.Units.Add(newunit);
            return _context.SaveChanges();
        }

		public int Delete(int id)
		{
			_context.Units.Remove(GetById(id));
			return _context.SaveChanges();
		}

		public List<Unit> GetAll()
		{
			return _context.Units.ToList();
		}

		public Unit GetById(int id)
		{
			return _context.Units.FirstOrDefault(c => c.Id == id);

		}

		public Unit GetByName(string Name)
		{
			return _context.Units.FirstOrDefault(c => c.Name.ToLower() == Name);
		}



		public int Update(int id, Unit newUnit)
		{

			var entry = _context.Entry(newUnit);
			entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return _context.SaveChanges();
		}
	}
}
