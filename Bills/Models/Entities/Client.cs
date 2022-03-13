using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bills.Models.Entities
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number :")]
        public int Id { get; set; }

       [Remote(action: "ClientNameUniqe", controller: "Clients", ErrorMessage = "Client Name has already existed before")]
        [Required(ErrorMessage = " Client NAME is Required ")]
        [Display(Name = "Client Name :")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Phone is Required ")]
        [RegularExpression("^[0-9]{10,14}$" ,ErrorMessage =" Please enter your phone number  ")]
        [Display(Name = "Phone :")]
        public int Phone { get; set; }

        [Required(ErrorMessage = " Address is Required ")]
        [Display(Name = "Address :")]
        public string Address { get; set; }
    }
}
