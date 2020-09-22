using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;

public class MongoDbRepository : IRepository
{
    private readonly IMongoCollection<Player> _playerCollection;
    private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

    public MongoDbRepository() {
        MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("game");
        _playerCollection = database.GetCollection<Player>("players");
         _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
    }
    public async Task<Player> Create(Player player)
    {
        await _playerCollection.InsertOneAsync(player);
        return player;
    }

    public async Task<Item> CreateItem(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        //try { 
            Player player = await _playerCollection.Find(filter).FirstAsync(); 
            player.items.Add(item);
            await _playerCollection.ReplaceOneAsync(filter, player);
            return item;
       //     }
/*             catch(InvalidOperationException){
                throw new NotFoundException(System.Net.HttpStatusCode.NotFound, "User not Found");
            } */

    }

    public async Task<Player> DeletePlayer(Guid playerId)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        return await _playerCollection.FindOneAndDeleteAsync(filter);
        
    }

    public async Task<Item> DeleteItem(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        foreach(Item i in player.items.ToList()) {
            if(i.id == item.id) {
                player.items.Remove(i);
            }
        }
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }

    public Task<Player> GetPlayer(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        return _playerCollection.Find(filter).FirstAsync();
        
    }

    public async Task<Player[]> GetAll()
    {
        List<Player> players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
        return players.ToArray();
    }

    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        Player player = await GetPlayer(playerId);
        return player.items.ToArray();
    }

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        Player player = await GetPlayer(playerId);
        foreach(Item i in player.items.ToList()) {
            if(i.id == itemId) {
                return i;
            }
        }
        return null;
    }

    public async Task<Player> UpdatePlayer(Player player)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return player;
    }

    public async Task<Item> UpdateItem(Guid playerId, Item item)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        for(int i = 0; i< player.items.Count ; i++) {
            if(player.items[i].id == item.id) {
                player.items[i] = item;
                Console.WriteLine("Item updated");
            }
        }
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }
}