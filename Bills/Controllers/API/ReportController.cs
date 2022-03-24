using Bills.Models.ModelView;
using Bills.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bills.Controllers.API
{
    [Route("api/Report/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public readonly ApiModel _apiModel;
        private readonly IItemService _itemService;


        public ReportController(IItemService itemService, ApiModel apiModel ) 
        {
            _apiModel = apiModel;
            _itemService = itemService;
   
        }
        public IActionResult Item()
        {
            _apiModel.Data = _itemService.getAll().Select(x => new ItemReportViewModel()
            {
                name = x.Name,
                total = x.BalanceOfTheFirstDuration,
                instock = x.QuantityRest
            });
            _apiModel.Success = true;
            return Ok(_apiModel);
        }
        
        [HttpGet("{name}")]
        public IActionResult Search(string name)
            {
                if (name == string.Empty || name == null)
                { return BadRequest(); }
                _apiModel.Data = _itemService.search(name);
                 _apiModel.Success = true;
                return Ok(_apiModel);
            }
        
    }
}
