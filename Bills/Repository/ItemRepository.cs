using Bills.Models;
using Bills.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Repository
{
	public class ItemRepository : IItemRepository
	{
		private readonly DatabaseContext _context;

		public ItemRepository(DatabaseContext context)
		{
			_context = context;
		}
		public int Add(Item newItem)
		{
			_context.Items.Add(newItem);
			return _context.SaveChanges();
		}

		public int Delete(int id)
		{
			_context.Items.Remove(GetById(id));
			return _context.SaveChanges();
		}

		public List<Item> GetAll()
		{
			return _context.Items.ToList();
		}

		public Item GetById(int id)
		{
			return _context.Items.FirstOrDefault(c => c.Id == id);
			
		}

		public Item GetByName(string Name)
		{
			return _context.Items.Include(c => c.TypeData).FirstOrDefault(c => c.Name.ToLower() == Name);

		}

		public int Update(int id, Item newItem)
		{
			
			var entry = _context.Entry(newItem);
			entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return _context.SaveChanges();
		}
	}
}
