using Bills.Models.Entities;
using System;
using System.Collections.Generic;

namespace Bills.Repository
{
    public interface IBillRepository : IRepository<Bill>
    {
        List<Bill> GetByDate(DateTime from, DateTime to);
    }
}
