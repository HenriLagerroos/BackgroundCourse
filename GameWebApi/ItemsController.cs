using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/players/{playerID}/items")]
public class ItemController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly IRepository _repo;
    public ItemController(ILogger<PlayerController> logger, IRepository repo) 
    {
        _logger = logger;
        _repo = repo;
    }

    [HttpPost("{id}")]
    public async Task<Item> Get(Guid playerID, Guid id)
    {
        return await _repo.GetItem(playerID, id);
    }

    [HttpGet]
    public async Task<Item[]> GetAll(Guid playerID)
    {
        return await _repo.GetAllItems(playerID);
    }

    [NotGoodEnoughFilter]
    [HttpPost("create")]
    public async Task<Item> Create(Guid playerID, [FromBody] NewItem newItem)
    {
        Player player = await _repo.GetPlayer(playerID);
            if (player == null || player.Id != playerID) throw new IdNotFoundException();

        if(player.Level < 3 && newItem.Type == ItemType.SWORD)    
            throw new NotGoodEnoughException("Player level is less than 3");
        Item item = new Item();
        item.Id = Guid.NewGuid();
        item.Level = newItem.Level;
        item.Type = newItem.Type;
        //item.CreationDate = newItem.CreationDate;     //To test, change frombody to NewItem class
        item.CreationDate = DateTime.Now;
        return await _repo.CreateItem(playerID, item);
    }

    [HttpPost("modify/{id}")]
    public async Task<Item> Modify(Guid playerID, Guid id, ModifiedItem modItem)
    {
        Item item = await _repo.GetItem(playerID, id);
        item.Level = modItem.Level;
        return await _repo.UpdateItem(playerID, item);
    }

    [HttpPost("delete/{id}")]
    public async Task<Item> Delete(Guid playerID, Guid id)
    {
        Item item = await _repo.GetItem(playerID, id);
        return await _repo.DeleteItem(playerID, item);
    }
}