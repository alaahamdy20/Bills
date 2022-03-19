using Bills.Models.Entities;
using Bills.Repository;
using Bills.Services.Interfaces;
using System.Collections.Generic;

namespace Bills.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepositroy _unitRepositroy;
        public UnitService( IUnitRepositroy unitRepositroy)
        {
            
            _unitRepositroy = unitRepositroy;
        }
        public int create(Unit unit)
        {
           return _unitRepositroy.Add(unit);
        }

        public List<Unit> getAll()
        {
           return _unitRepositroy.GetAll();
        }

        public bool Unique(string Name)
        {
            Unit unit = _unitRepositroy.GetByName(Name);
            if (unit != null)
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
