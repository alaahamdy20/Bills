using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bills.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }

        [Remote(action: "ItemNameUniqe", controller: "Items", AdditionalFields = "CompanyId", ErrorMessage = "Item Name has already existed before")]
        [Required(ErrorMessage = " Item NAME is Required ")]
        [Display(Name = "Item Name : ")]
        public string Name { get; set; }

        [Range(0,int.MaxValue, ErrorMessage = "SELLING PRICE Must be Greater than or equal Zero")]
        [Display(Name = " selling price:")]
        public int SellingPrice { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "BUYING PRICE Must be Greater than or equal Zero")]
        [Remote(action: "BuyingPriceCondition", controller: "Items", AdditionalFields = "SellingPrice", ErrorMessage = "BUYING PRICE Must be less than or equal SELLING PRICE")]
        [Display(Name = " buying Price :")]
        public int BuyingPrice { get; set; }


        public string Notes { get; set; }

        [Remote(action: "RequiredCompany", controller: "Items", ErrorMessage = "COMPANY NAME is Required")]
        [ForeignKey("CompanyData")]
        [Display(Name = "Company Name: ")]
        public int CompanyId { get; set; }


        [Remote(action: "RequiredType", controller: "Items", ErrorMessage = "TYPE NAME is Required")]
        [ForeignKey("TypeData")]
        [Display(Name = "Type Name: ")]
        public int typeId { get; set; }

    
        [Remote(action: "RequiredUnit", controller: "Items", ErrorMessage = " Unit NAME is Required")]
        [ForeignKey("Unit")]
        [Display(Name = "Unit: ")]
        public int unitId { get; set; }

        [Required(ErrorMessage = " Balance of the first duration is Required ")]
        [Display(Name = "Balance Of The First Duration ")]
        [Range(1, int.MaxValue, ErrorMessage = "Balance of the first duration Must be Greater than Zero")]
        public int BalanceOfTheFirstDuration { get; set; }

        [JsonIgnore]
        public virtual CompanyData CompanyData { get; set; }
        [JsonIgnore]
        public virtual TypeData TypeData { get; set; }
        [JsonIgnore]
        public virtual Unit Unit { get; set; }
        
        public int QuantityRest { get; set; }



    }
}
