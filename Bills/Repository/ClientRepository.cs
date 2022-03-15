using Bills.Models;
using Bills.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bills.Repository
{
	public class ClientRepository : IClientRepository
	{
		private readonly DatabaseContext _context;

		public ClientRepository(DatabaseContext context)
		{
			_context = context;
		}
		public int Add(Client newClient)
		{
			_context.Clients.Add(newClient);
			return _context.SaveChanges();
		}

		public int Delete(int id)
		{
			_context.Clients.Remove(GetById(id));
			return _context.SaveChanges();
		}

		public List<Client> GetAll()
		{
			return _context.Clients.ToList();
		}

		public Client GetById(int id)
		{
			return _context.Clients.FirstOrDefault(c => c.Id == id);

		}

		public Client GetByName(string Name)
		{
			return _context.Clients.FirstOrDefault(c => c.Name.ToLower() == Name);

		}

		public int Update(int id, Client newClient)
		{

			var entry = _context.Entry(newClient);
			entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			return _context.SaveChanges();
		}
	}
}
