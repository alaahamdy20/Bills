using Bills.Models;
using Bills.Models.Entities;
using Bills.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Repository
{
	public class BillItemRepository : IBillItemRepository
	{
		private readonly DatabaseContext _context;

		public BillItemRepository(DatabaseContext context)
		{
			_context = context;
		}
		public int Add(BillItem newBillItem)
		{
			_context.BillItems.Add(newBillItem);
			return _context.SaveChanges();
		}

		public int Delete(int id)
		{
			_context.BillItems.Remove(GetById(id));
			return _context.SaveChanges();
		}

		public List<BillItem> GetAll()
		{
			return _context.BillItems.ToList();
		}

		public BillItem GetById(int id)
		{
			return _context.BillItems.FirstOrDefault(c => c.Id == id);

		}



		public int Update(int id, BillItem newBillItem)
		{

			var entry = _context.Entry(newBillItem);
			entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return _context.SaveChanges();
		}
	}
}
