using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bills.Models.Entities;
using Bills.Models.ModelView;
using Bills.Services.Interfaces;

namespace Bills.Controllers.API
{
    [Route("api/TypeData/[action]")]
    [ApiController]
    public class TypeDatasController : ControllerBase
    {
    
        private readonly ITypeDataService _typeDataService;
        public readonly ApiModel _apiModel;

        public TypeDatasController(ITypeDataService typeDataService, ApiModel apiModel)
        {
            
            _typeDataService = typeDataService;
            _apiModel = apiModel;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Type()
        {
            _apiModel.Data = _typeDataService.getAll();
            _apiModel.Success = true;
            return Ok(_apiModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Type(int id)
        {
            TypeData typeData = _typeDataService.getById(id);
            if (typeData == null)
            {
                _apiModel.Data = null;
                _apiModel.Success = false;
                _apiModel.Message = "this Id not found ";
                return NotFound(_apiModel);
            }
            else
            {
                _apiModel.Data = typeData;
                _apiModel.Success = true;
                return Ok(_apiModel);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult TypesByCompanyId(int id)
        {
            List<TypeData> typeData = _typeDataService.getByCompanyId(id);
            if (typeData == null)
            {
                _apiModel.Data = null;
                _apiModel.Success = false;
                _apiModel.Message = "no types for this company ";
                return NotFound(_apiModel);
            }
            else
            {
                _apiModel.Data = typeData;
                _apiModel.Success = true;
                return Ok(_apiModel);
            }
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 201)]
        public IActionResult Type(TypeView typeView)
        {
            if (ModelState.IsValid)
            {
                if (typeView.CompanyId == 0)
                {
                    _apiModel.Success=false;
                    _apiModel.Message = "COMPANY NAME is Required";
                    _apiModel.Data=null;
                    return BadRequest(_apiModel);
                }
                else
                {

                    _apiModel.Success = true;
                    _apiModel.Message = " ";
                    _apiModel.Data = _typeDataService.create(typeView); 
                    return Ok(_apiModel);
                }
            }
            _apiModel.Success = false;
            _apiModel.Message = "validation error ";
            _apiModel.Data = null;
            return BadRequest(_apiModel);
        }

       



    }
}
