using Microsoft.AspNetCore.Mvc;
using Quartile.Stores.Domain.Dtos;
using Quartile.Stores.Domain.Dtos.Endpoints.Store;
using Quartile.Stores.Domain.Interfaces.Services;
using Quartile.Stores.Domain.Models;

namespace Quartile.Stores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StoreModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult<IEnumerable<StoreModel>> GetStores(int comanyId)
        {
            var result = _storeService.GetAll(comanyId);

            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult<StoreDto> GetStore(int id)
        {
            var result = _storeService.GetById(id);

            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult<StoreModel> CreateStore([FromBody] CreateStoreRequest store)
        {
            var result = _storeService.Create(store);

            if (result.Success)
                return Created();
            else
                return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult UpdateStore(int id, StoreDto store)
        {
            var result = _storeService.Update(id, store);

            if (result.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult DeleteStore(int id)
        {
            var result = _storeService.Delete(id);

            if (result.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }
}
