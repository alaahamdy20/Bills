using Microsoft.AspNetCore.Mvc;
using Bills.Models.Entities;
using Bills.Services.Interfaces;
using Bills.Models.ModelView;
using System.Collections.Generic;

namespace Bills.Controllers.API
{
    [Route("api/Units/[action]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;
        public readonly ApiModel _apiModel;

        public UnitsController( IUnitService unitService, ApiModel apiModel)
        {
            _unitService = unitService;
            _apiModel = apiModel;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Unit()
        {
            _apiModel.Data = _unitService.getAll();
            _apiModel.Success = true;
            return Ok(_apiModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Unit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _apiModel.Success = true;
                _apiModel.Data = _unitService.create(unit); 
                return Ok(_apiModel);
               
            }
            _apiModel.Success = false;
            _apiModel.Message = "validation error ";
            _apiModel.Data = null;
            return BadRequest(_apiModel);
        }

       
    }
}
