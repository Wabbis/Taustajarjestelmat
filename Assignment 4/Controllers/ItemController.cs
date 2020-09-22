using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

 [Route("api/player/{playerId:Guid}/items/")]
public class ItemController : Controller {

    private readonly IRepository _irep; 
    private readonly ILogger<ItemController> _logger;
    public ItemController(IRepository repository, ILogger<ItemController> logger) { 
        _irep = repository;
        _logger = logger;
     }


    [HttpPost("create")]
    public async Task<Item> CreateItem(Guid playerId, [FromBody] NewItem item) {
        Item itemToAdd = new Item();
        itemToAdd.name = item.name;
        itemToAdd.id = Guid.NewGuid();
        await _irep.CreateItem(playerId, itemToAdd);
        return itemToAdd;
    }

    [HttpGet("Get/{itemId:guid}")]
    public async Task<Item> GetItem(Guid playerId, Guid itemId) {
        return await _irep.GetItem(playerId, itemId);;
    }

    [HttpGet("allitems")]
    public async Task<Item[]> GetAllItems(Guid playerId) {
        Item[] items = await _irep.GetAllItems(playerId);
        return items;
    }

    [HttpPost("updateitem")]
    public async Task<Item> UpdateItem(Guid playerId,[FromBody] Item item) {
        return await _irep.UpdateItem(playerId, item);
        
    }

    [HttpDelete("delete")]
    public async Task<Item> DeleteItem(Guid playerId, [FromBody] Item item) {
        await _irep.DeleteItem(playerId, item);
        return null;
    }

}