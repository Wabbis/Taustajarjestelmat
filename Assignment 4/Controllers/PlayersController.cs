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

[Route("api/player/")]
public class PlayersController : Controller {
    private readonly IRepository _irep; 
    private readonly ILogger<PlayersController> _logger;
    public PlayersController(IRepository repository, ILogger<PlayersController> logger) { 
        _irep = repository;
        _logger = logger;
     }

    [HttpGet("{id:Guid}")]
    public Task<Player> Get(Guid id){
        _irep.GetPlayer(id);
        return null;
    }

    [HttpGet("all")]
    public Task<Player[]> GetAll(){
        return _irep.GetAll();
    }

    [HttpPost("create")]
    public async Task<Player> Create([FromBody] NewPlayer newPlayer){
        Player player = new Player();
        player.Id = Guid.NewGuid();
        player.Name = newPlayer.Name;
        await _irep.Create(player);
        return player;
    }

    [HttpPost("modify/{id:Guid}")]
    public Task<Player> Modify(Guid id, [FromBody] Player player){
        _irep.UpdatePlayer(player);
        return null;
    }


    [HttpDelete("delete/{id}")]
    public async Task<Player> Delete(Guid id) {
        await _irep.DeletePlayer(id);
        return null;
    }




}