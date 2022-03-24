using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bills.Models.Entities;
using Bills.Services.Interfaces;
using Bills.Models.ModelView;

namespace Bills.Controllers.API
{
    [Route("api/Items/[action]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
 
        private readonly IItemService _itemService;
        public readonly ApiModel _apiModel;

        public ItemsController( IItemService itemService, ApiModel apiModel)
        {
            _apiModel = apiModel;
            _itemService = itemService;
        }

        public IActionResult Item()
        {
            _apiModel.Data = _itemService.getAll();
            _apiModel.Success = true;
            return Ok(_apiModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 200)]
        public IActionResult Item(int id)
        {
          Item item  = _itemService.getById(id);
            if (item == null)
            {
                _apiModel.Data = null;
                _apiModel.Success = false;
                _apiModel.Message = "no found any item for this ID ";
                return NotFound(_apiModel);
            }
            else
            {
                _apiModel.Data = item;
                _apiModel.Success = true;
                return Ok(_apiModel);
            }
        }

        [HttpPost]
       
        [ProducesResponseType(typeof(IEnumerable<ApiModel>), 201)]
        public IActionResult Item(Item item)
        {
            if (ModelState.IsValid)
            {
                if (!_itemService.Unique(item.Name))
                {
                    _apiModel.Success = false;
                    _apiModel.Message = "this Item already exist ";
                    _apiModel.Data = null;
                    return BadRequest(_apiModel);
                }
                else
                {
                    item.Id = 0;
                    item.QuantityRest = item.BalanceOfTheFirstDuration;
                    _apiModel.Success = true;
                    _apiModel.Data = _itemService.create(item);
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
