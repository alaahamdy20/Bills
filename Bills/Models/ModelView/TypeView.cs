using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bills.Models.ModelView
{
    public class TypeView
    {
        [Required(ErrorMessage = " COMPANY NAME is Required ")]
        [Display(Name = "COMPANY NAME")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = " TYPE NAME is Required ")]
        [Display(Name = "TYPE NAME")]
        [Remote(action: "TypeNameUniqe", controller: "TypeDatas", AdditionalFields = "CompanyId", ErrorMessage = "TYPE NAME has already existed before")]
        public string Name { get; set; }

        public string Notes { get; set; }
    }
}
