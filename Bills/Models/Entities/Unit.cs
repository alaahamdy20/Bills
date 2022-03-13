using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bills.Models.Entities
{
    public class Unit
    {

        public int Id { get; set; }

        [Display(Name = "Unit Name :")]
        [Remote(action: "unitNameUniqe", controller: "Units", ErrorMessage = "Unit Name has already existed before")]
        [Required(ErrorMessage = " Unit NAME is Required ")]
        public string Name { get; set; }
        public string Notes { get; set; }

    }
}
