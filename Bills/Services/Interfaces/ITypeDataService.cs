using Bills.Models.Entities;
using Bills.Models.ModelView;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface ITypeDataService
    {

        public List<TypeData> getAll();
        public List<TypeData> getByCompanyId(int companyId);
        public int create(TypeView typeView);

        public bool Unique(string Name , int CompanyId);
    }
}
