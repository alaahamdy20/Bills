using Bills.Models;
using Bills.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Repository
{
	public class BillRepository : IBillRepository
	{
		private readonly DatabaseContext _context;

		public BillRepository(DatabaseContext context)
		{
			_context = context;
		}
		public int Add(Bill newBill)
		{
			_context.Bills.Add(newBill);
			return _context.SaveChanges();
		}

		public int Delete(int id)
		{
			_context.Bills.Remove(GetById(id));
			return _context.SaveChanges();
		}

		public List<Bill> GetAll()
		{
			return _context.Bills.ToList();
		}

		public Bill GetById(int id)
		{
			return _context.Bills.FirstOrDefault(c => c.Id == id);

		}
		public List<Bill> GetByDate(DateTime from ,DateTime to)
		{
			return _context.Bills.Where(b => b.BillDate >= from && b.BillDate < to).ToList();

		}



		public int Update(int id, Bill newBill)
		{

			var entry = _context.Entry(newBill);
			entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return _context.SaveChanges();
		}
	}
}
