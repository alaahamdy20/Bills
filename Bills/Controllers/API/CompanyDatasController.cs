using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bills.Models.Entities;
using Bills.Services.Interfaces;
using Bills.Models.ModelView;


namespace Bills.Controllers.API
{
    [Route("api/CompanyData/[action]")]
    [ApiController]
    public class CompanyDatasController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public readonly ApiModel _apiModel;

        public CompanyDatasController(ICompanyService companyService, ApiModel apiModel) {
            _companyService = companyService;
            _apiModel = apiModel;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Company()
        {
            _apiModel.Data = _companyService.getAll();
            _apiModel.Success = true;
            return Ok(_apiModel);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Company(int id)
        {
            CompanyData company = _companyService.getById(id);
            if (company == null)
            {
                _apiModel.Data = null;
                _apiModel.Success = false;
                _apiModel.Message = "this Id not found ";
                return NotFound(_apiModel);
            }
            else
            {
                _apiModel.Data = company;
                _apiModel.Success = true;
                return Ok(_apiModel);
            }
        }


        [HttpPost]
        
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 201)]
        public IActionResult Company( CompanyData companyData)
        {
            if (ModelState.IsValid)
            {
                companyData.Id = 0;
                _apiModel.Data = _companyService.create(companyData);
                _apiModel.Success = true;
                return Ok(_apiModel);
            }
            _apiModel.Data = companyData;
            _apiModel.Success = false;
            _apiModel.Message = "Validation Error";
            return BadRequest(_apiModel);
        }
       
    }
}
