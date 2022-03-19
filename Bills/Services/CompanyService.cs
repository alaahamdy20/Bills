using Bills.Models.Entities;
using Bills.Repository;
using Bills.Services.Interfaces;
using System.Collections.Generic;

namespace Bills.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository; 
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository= companyRepository;
        }

        public int create(CompanyData companyData)
        {
           return _companyRepository.Add(companyData);
        }

        public List<CompanyData> getAll()
        {
           return _companyRepository.GetAll();
        }

        public bool Unique(string Name)
        {
           CompanyData companyData =  _companyRepository.GetByName(Name);
            if (companyData != null)
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
