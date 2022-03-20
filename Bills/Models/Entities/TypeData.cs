using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Bills.Models.Entities
{
    public class TypeData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        [JsonIgnore]
        public virtual ICollection<CompanyType> CompanyTypes { get; set; }

    }
}
