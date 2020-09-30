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

    [HttpGet("{id:Guid}")]
    public async Task<Player> Get(Guid id)
    {
        Player player = await _repo.GetPlayer(id);
        return player;
    }

    [HttpGet("{name:alpha}")]
    public async Task<Player> Get(string name)
    {
        Player player = await _repo.GetPlayer(name);
        return player;
    }

    [HttpGet("top10")]
    public async Task<Player[]> GetTopTenDescending(int? minScore)
    {
        return await _repo.GetTop10SortedByScoreDescending();
    }

    [HttpGet("common")]
    public async Task<int> GetMostCommon()
    {
        return await _repo.GetMostCommonLevel();
    }

    [HttpGet("avgScore/{a}/to/{b}")]
    public async Task<float> GetAvgScoresDate(DateTime a, DateTime b)
    {
        return await _repo.GetAvgScoreBetweenDates(a, b);
    }

    [HttpGet("countsForPrice/{price}")]
    public async Task<int> GetCountsForPrice(int price)
    {
        return await _repo.GetItemCountsForPrice(price);
    }

    [HttpGet]
    public async Task<Player[]> GetAll(int? minScore)
    {
        if (minScore.HasValue)
            return await _repo.GetAllMinScore(minScore.Value);
        else
            return await _repo.GetAllPlayers();
    }

    [HttpGet("tag/{tag}")]
    public async Task<Player[]> GetAllWithTag(string tag)
    {
        return await _repo.GetAllTag(tag);
    }
    
    [HttpGet("weapon/{type}")]
    public async Task<Player[]> GetAllWithTag(int type)
    {
        return await _repo.GetAllItemType((ItemType)type);
    }

    [HttpGet("itemCount/{count}")]
    public async Task<Player[]> GetAllWithItemCount(int count)
    {
        return await _repo.GetAllItemCount(count);
    }

    [HttpPost("create")]
    public async Task<Player> Create([FromBody] NewPlayer newPlayer)
    {
        Player player = new Player();
        player.Id = Guid.NewGuid();
        player.Name = newPlayer.Name;
        return await _repo.CreatePlayer(player);
    }

    [HttpPost("rename/{id}/{name}")]
    public async Task<Player> Delete(Guid id, string name)
    {
        return await _repo.Rename(id, name);
    }

    [HttpPost("incScore/{id}/{increment}")]
    public async Task<Player> IncrementPlayerScore(Guid id, int increment)
    {
        return await _repo.IncrementPlayerScore(id, increment);
    }

    [HttpPost("modify/{id}")]
    public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
    {
        Player player = await _repo.GetPlayer(id);
        player.Score = modifiedPlayer.Score;
        player.Level = modifiedPlayer.Level;
        if(modifiedPlayer.AddTag != null && modifiedPlayer.AddTag.Length > 0)
            player.Tags.Add(modifiedPlayer.AddTag);
        return await _repo.UpdatePlayer(player);
    }

    [HttpPost("delete/{id}")]
    public async Task<Player> Delete(Guid id)
    {
        return await _repo.DeletePlayer(id);
    }
}