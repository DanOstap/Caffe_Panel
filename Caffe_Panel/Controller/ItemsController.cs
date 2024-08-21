using Caffe_Panel.DataBase.Service;
using Caffe_Panel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caffe_Panel.Controller
{
    [Route("/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService itemsService;
        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItems() {
            var items = itemsService.FindAll();
            return Ok(items);
        }
        [HttpGet("/byId")]
        public async Task<ActionResult<Items>> GetItemById(int id) {
            var item = await itemsService.FindOneById(id);
            if (item == null) {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpGet("/byName")]
        public async Task<ActionResult<Items>> GetItemByName(string name) {
            var item = await itemsService.FindOneByName(name);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpGet("/byDiscount")]
        public async Task<ActionResult<Items>> GetItemsByDiscount(int range_from, int range_to) {
            var items = await itemsService.FindOneByDiscount(range_from, range_to);
            if (items == null) {
                return BadRequest();
            }
            return Ok(items);
        }
        [HttpPost]
        public async Task<ActionResult<Items>> CreateItem([FromBody] Items _model)
        {
            var item = await itemsService.CreateItem(_model);
            return Ok(item);
        }
        [HttpPut]
        public async Task<ActionResult<Items>> UpdateItem(int id, [FromBody] Items _model) {
            var item = await itemsService.Update(id, _model);
            if (item == null) {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpDelete("/byId")]
        public async Task<IActionResult> DeleteItemById(int id) {
            var item = await itemsService.RemoveById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpDelete("/byName")]
        public async Task<IActionResult> DeleteItemByName(string name )
        {
            var item = await itemsService.RemoveByName(name);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
