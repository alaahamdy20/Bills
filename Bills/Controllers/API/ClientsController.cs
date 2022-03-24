using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bills.Models.Entities;
using Bills.Services.Interfaces;
using Bills.Models.ModelView;
using System.Collections.Generic;

namespace Bills.Controllers.API
{
    [Route("api/Clients/[action]")]
    [ApiController]
    [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
    public class ClientsController : ControllerBase
    {

        public readonly ApiModel _apiModel;
        private readonly IClientService _clientService;


        public ClientsController(ApiModel apiModel, IClientService clientService)
        {
            _apiModel = apiModel;
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult Client()
        {
            _apiModel.Data = _clientService.getAll();
            _apiModel.Success = true;
            return Ok(_apiModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Client(int id)
        {
           Client client= _clientService.getById(id);
            if (client == null)
            {
                _apiModel.Data = null;
                _apiModel.Success = false;
                _apiModel.Message = "no found any clint for this ID ";
                return NotFound(_apiModel);
            }
            else
            {
                _apiModel.Data = client;
                _apiModel.Success = true;
                return Ok(_apiModel);
            }
        }




        [HttpPost]
       public IActionResult Client(Client client)
        {
            if (ModelState.IsValid)
            {
                if (!_clientService.Unique(client.Name))
                {
                    _apiModel.Success = false;
                    _apiModel.Message = "clint already exist ";
                    _apiModel.Data = null;
                    return BadRequest(_apiModel);
                }
                else
                { 
                    client.Id = 1 + _clientService.getAll().Count();
                    _apiModel.Data = _clientService.create(client);
                    _apiModel.Success = true;
                    _apiModel.Message = "  Client added successfully";
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
