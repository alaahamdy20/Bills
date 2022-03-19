using Bills.Models.Entities;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface ICompanyService 
    {
        public bool Unique( string Name);

        public List<CompanyData> getAll();
        public int create(CompanyData companyData); 


    }
}
