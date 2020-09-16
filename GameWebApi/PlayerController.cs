using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/players")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly IRepository _repo;
    public PlayerController(ILogger<PlayerController> logger, IRepository repo) 
    {
        _logger = logger;
        _repo = repo;
    }

    [HttpPost("{id}")]
    public async Task<Player> Get(Guid id)
    {
        return await _repo.Get(id);
    }

    [HttpGet]
    public async Task<Player[]> GetAll()
    {
        return await _repo.GetAll();
    }

    [HttpPost("create")]
    public async Task<Player> Create([FromBody] NewPlayer newPlayer)
    {
        Player player = new Player();
        player.Id = Guid.NewGuid();
        player.Name = newPlayer.Name;
        return await _repo.Create(player);
    }


    [HttpPost("modify/{id}")]
    public async Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player)
    {
        return await _repo.Modify(id, player);
    }

    [HttpPost("delete/{id}")]
    public async Task<Player> Delete(Guid id)
    {
        return await _repo.Delete(id);
    }
}