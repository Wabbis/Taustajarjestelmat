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
    
    public async Task<Player[]> GetPlayerWithHigherScoreThan(int score) {
        var filter = Builders<Player>.Filter.Gt(p => p.Score, score);
        List<Player> list = await _playerCollection.Find(filter).ToListAsync();
        return list.ToArray();
    }

    public async Task<Player> GetPlayerWithName(string name) {
        var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
        return await _playerCollection.Find(filter).FirstAsync();
    }

    public async Task<Player[]> GetPlayersWithItemSize(int size) {
        var filter = Builders<Player>.Filter.Eq(p => p.items.Count, size);
        List<Player> list = await _playerCollection.Find(filter).ToListAsync();
        return list.ToArray();
    }

    public async Task<Player> UpdatePlayerNameWithoutFetch(Guid playerId, string newName) {
        
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        await _playerCollection.FindOneAndUpdateAsync(
            filter,
            Builders<Player>.Update.Set(p => p.Name, newName)
        );
        return null;
    }

    public async Task<Player> IncrementPlayerWithoutFetch(Guid playerId, int incrementValue)
    {
        var options = new FindOneAndUpdateOptions<Player>() {
            ReturnDocument = ReturnDocument.After
        };
        return await _playerCollection.FindOneAndUpdateAsync(
            Builders<Player>.Filter.Eq(p => p.Id, playerId),
            Builders<Player>.Update.Inc(p => p.Score, incrementValue), 
            options 
        ); 
        
    }

    public async Task<Player> PushItemToPlayer(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        var push = Builders<Player>.Update.Push(p => p.items, item);
        return await _playerCollection.FindOneAndUpdateAsync(filter, push);
        
    }

    public async Task<Player> RemoveItemAndIncrementScore(Guid playerId, Guid itemId, int score)
    {
        var removeItem = Builders<Player>.Update.PullFilter(p => p.items, i => i.id == itemId);
        var increment = Builders<Player>.Update.Inc(p => p.Score, score);
        var update = Builders<Player>.Update.Combine(removeItem, increment);

        return await _playerCollection.FindOneAndUpdateAsync(
            Builders<Player>.Filter.Eq(p => p.Id, playerId),
            update
        );
    }

    public async Task<Player[]> SortTop10PlayersDescending()
    {
        var initSort = Builders<Player>.Sort.Descending(p => p.Score);
        List<Player> list = await _playerCollection.Find(new BsonDocument()).Limit(10).Sort(initSort).ToListAsync();
        return list.ToArray();
    }

    public Task<Player[]> FindPlayersWithTag(string tag)
    {
        throw new NotImplementedException();
    }

    /*     public async Task<Player[]> FindPlayersWithTag(string tag)
        {
            var filter = Builders<Player>.Filter.ElemMatch<String>(
                p => p.tags,
                Builders<String>.Filter.Eq(t => t.ToString(), tag)
                );
            var filters = Builders<Player>.Filter.AnyEq(p => p.tags, tag);
            return null;
        } */


}