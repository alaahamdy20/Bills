using Bills.Models.Entities;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface IClientService
    {
        public bool Unique(string Name);

        public List<Client> getAll();
        public int create(Client client);
    }
}
