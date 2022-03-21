using Bills.Models.Entities;
using System;
using System.Collections.Generic;

namespace Bills.Services.Interfaces
{
    public interface IReportService
    {
        public List<Bill> GetByDate(DateTime from, DateTime to);
    }
}
