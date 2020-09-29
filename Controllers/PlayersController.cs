using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[Route("api/player")]
public class PlayersController : Controller {
    private readonly IRepository _irep; 
    private readonly ILogger<PlayersController> _logger;
    public PlayersController(IRepository repository, ILogger<PlayersController> logger) { 
        _irep = repository;
        _logger = logger;
     }

    [HttpGet("/{id:Guid}")]
    public async Task<Player> Get(Guid id){
        return await _irep.GetPlayer(id);
    }
    [HttpGet("/{name}")]
    public async Task<Player> GetPlayerWithName(string name) {
        return await _irep.GetPlayerWithName(name);
    } 

    [HttpGet("/all")]
    public async Task<Player[]> GetAll(){
        return await _irep.GetAll();
    }

    [HttpPost("/create")]
    public async Task<Player> Create([FromBody] NewPlayer newPlayer){
        Player player = new Player();
        player.Id = Guid.NewGuid();
        player.Name = newPlayer.Name;
        await _irep.Create(player);
        return player;
    }

    [HttpPost("/modify/{id:Guid}")]
    public Task<Player> Modify(Guid id, [FromBody] Player player){
        _irep.UpdatePlayer(player);
        return null;
    }


    [HttpDelete("/delete/{id}")]
    public async Task<Player> Delete(Guid id) {
        await _irep.DeletePlayer(id);
        return null;
    }

    [HttpGet("/score/higherthan{x:int}")]
    public async Task<Player[]> GetPlayerWithHigherScoreThan(int x) {
        return await _irep.GetPlayerWithHigherScoreThan(x);        
    }
    [HttpGet("/itemsize={size:int}")]
    public async Task<Player[]> GetPlayersWithItemSize(int size) {
        return await _irep.GetPlayersWithItemSize(size);
    }
    [HttpPost("update/{playerId:Guid}")]
    public async Task<Player> UpdatePlayerNameWithoutFetch(Guid playerId,[FromBody] string newName) {
        return await _irep.UpdatePlayerNameWithoutFetch(playerId, newName);
    }
    [HttpPost("/score/{playerId:Guid}")]
    public async Task<Player> IncrementPlayerWithoutFetch(Guid playerId, [FromBody] int incrementValue) {
        return await _irep.IncrementPlayerWithoutFetch(playerId, incrementValue);
    }
    [HttpPost("/score/{playerId:Guid}/{itemId:Guid}")]
    public async Task<Player> RemoveItemAndIncrementScore(Guid playerId, Guid itemId, [FromBody] int score) {
        return await _irep.RemoveItemAndIncrementScore(playerId, itemId, score);
    }
    [HttpGet("/sort10")]
    public async Task<Player[]> SortTop10PlayersDescending() {
        return await _irep.SortTop10PlayersDescending();
    }
    

}
