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

[Route("/api/players/")]
public class PlayersController : Controller {
    IRepository _irep;
    public PlayersController(IRepository repository) { 
        _irep = repository;
     }

    [HttpGet("{id}")]
    public Task<Player> Get(Guid id){
        _irep.Get(id);
        return null;
    }

    [HttpGet]
    public Task<ListofPlayers> GetAll(){
        return _irep.GetAll();
    }


    [HttpPost("{Player}")]
    public Task<Player> Create(Player player){
        player.CreationTime = DateTime.Now;
        player.Score = 0;
        player.Level = 1; 
        player.IsBanned = false;
        player.Id = Guid.NewGuid(); 
        return null;
    }


    [HttpPost("{id, player}")]
    public Task<Player> Modify(Guid id, ModifiedPlayer player){
        return null;
    }


    [HttpDelete("{id}")]
    public Task<Player> Delete(Guid id) {
        return null;
    }
}