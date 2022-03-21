using Bills.Models.Entities;
using Bills.Repository;
using Bills.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Bills.Services
{
    public class ReportService : IReportService
    {
        private readonly IBillRepository _billRepository;
    
        public ReportService(IBillRepository billRepository)
        {
          
            _billRepository = billRepository;
        }
        public List<Bill> GetByDate(DateTime from, DateTime to)
        {
            return _billRepository.GetByDate(from, to);
        }
    }
}
