using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bills.Models.Entities
{

    public class CompanyData
    {
   
        public int Id { get; set; }

        [Required(ErrorMessage = " COMPANY NAME is Required ") ]
        [Remote(action: "CompanyNameUniqe", controller: "CompanyDatas", ErrorMessage = "COMPANY NAME has already existed before")]
        public string Name { get; set; }

        public string Notes { get; set; }

        [JsonIgnore]
        public virtual ICollection<CompanyType> CompanyTypes { get; set; }


    }
}
