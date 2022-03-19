using Bills.Models.Entities;
using Bills.Models.ModelView;
using Bills.Repository;
using Bills.Services.Interfaces;
using System.Collections.Generic;

namespace Bills.Services
{
    public class TypeDataService : ITypeDataService
    {
        private readonly ITypeRepository _typeRepository;
        private readonly ICompanyTypeRepository _CompanyTypeRepository;
        public TypeDataService(ITypeRepository typeRepository, ICompanyTypeRepository CompanyTypeRepository) { 
            _typeRepository = typeRepository;
            _CompanyTypeRepository= CompanyTypeRepository;
        }

        public List<TypeData> getAll()
        {
         return _typeRepository.GetAll();
        }

        public int create(TypeView typeView)
        {
            TypeData OldtypeData =_typeRepository.GetByName(typeView.Name);
            if (OldtypeData == null)
            {
                TypeData typeData = new TypeData();
                typeData.Name = typeView.Name;
                typeData.Notes = typeView.Notes;
                _typeRepository.Add(typeData);
            }
            TypeData NewTypeData = _typeRepository.GetByName(typeView.Name);
            CompanyType companyType = new CompanyType();
            companyType.CompanyDataId = typeView.CompanyId;
            companyType.TypeDataId = NewTypeData.Id;
          return  _CompanyTypeRepository.Add(companyType);
        }

     

        public bool Unique(string Name , int CompanyId)
        {
            int typeDataID = _typeRepository.GetByName(Name).Id;
            CompanyType companyType = _CompanyTypeRepository.getByIds(CompanyId, typeDataID);
            if (companyType != null)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        public List<TypeData> getByCompanyId(int companyId)
        {
          return _typeRepository.getByCompanyId(companyId);
        }
    }
}
