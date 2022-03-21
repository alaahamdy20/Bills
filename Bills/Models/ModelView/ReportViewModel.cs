using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bills.Models.ModelView
{
    public class ReportViewModel
    {
        [Required]
        public DateTime FromDate { get; set; }
       
        public DateTime? ToDate { get; set; }
    }
}
