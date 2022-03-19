using Bills.Models.Entities;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface IUnitService
    {
        public bool Unique(string Name);

        public List<Unit> getAll();
        public int create(Unit unit);
    }
}
