using Bills.Models.Entities;
using Bills.Repository;
using Bills.Services.Interfaces;
using System.Collections.Generic;

namespace Bills.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;
        public ClientService( IClientRepository clientRepository)
        {

            _clientRepository = clientRepository;
        }
        public int create(Client client)
        {
           return _clientRepository.Add(client);
        }

        public List<Client> getAll()
        {
         return _clientRepository.GetAll();  
        }

        public bool Unique(string Name)
        {
            Client client = _clientRepository.GetByName(Name);
            if (client != null)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
    }
}
