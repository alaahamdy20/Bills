using System.ComponentModel.DataAnnotations.Schema;

namespace Bills.Models.Entities
{
    public class CompanyType
    {

        [ForeignKey("TypeData")]
        public int TypeDataId { get; set; }
        public virtual TypeData TypeData { get; set; }


        [ForeignKey("CompanyData")]
        public int CompanyDataId { get; set; }
        public virtual CompanyData CompanyData { get; set; }

    }
}
