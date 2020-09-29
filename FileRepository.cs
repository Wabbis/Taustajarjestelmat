using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace FileRepos
{
    public class FileRepository : IRepository {
        public async Task<Player> GetPlayer(Guid playerId){
            Player[] list = await GetAll();
            foreach(Player p in list)  {
                if(p.Id == playerId) {
                    return p;
                }
            } 
           return null;
        }
        public async Task<Player[]> GetAll() {

            ListofPlayers playerlist = await ReadFile();
            return playerlist.players.ToArray();
        }

        public async Task<Player> Create(Player player) {

            player.IsBanned = false;
            player.CreationTime = DateTime.UtcNow;
            player.Level = 0;
            player.Score = 0;

            ListofPlayers allPlayers = await ReadFile();
            allPlayers.players.Add(player);

            await WriteFile(allPlayers);      
            return player;
        }

        public Task<Player> UpdatePlayer(Player player) {
            
            return null;
        }

        public async Task<Player> DeletePlayer(Guid playerId){
            ListofPlayers list = await ReadFile();
            Player[] loopList = list.players.ToArray();
            foreach(Player p in loopList) {
                if(p.Id == playerId) {
                    Console.WriteLine("Player " + p.Name + " removed");
                    list.players.Remove(p);
                }
            }
            Console.WriteLine(list.players.Count);
            for(int i = 0; i < list.players.Count ; i++) {
                Console.WriteLine(list.players[i].Name);
            }
            await WriteFile(list);

            return null;
        }

        public async Task<Item> CreateItem(Guid playerId, Item item) {
            Player player = await GetPlayer(playerId);
            player.items.Append(item); 
            return null;
        }
        public async Task<Item> GetItem(Guid playerId, Guid itemId){
            Item[] playerItems = await GetAllItems(playerId);
            foreach(Item i in playerItems) 
            {
                if(i.id == itemId) {
                    return i;
                }
            } 
            return null;
        }
        public async Task<Item[]> GetAllItems(Guid playerId){
            Player player = await GetPlayer(playerId);  
            return player.items.ToArray();
        }
        public async Task<Item> UpdateItem(Guid playerId, Item item){
            ListofPlayers listofplayers = await ReadFile();
            foreach(Player p in listofplayers.players) {
                if(p.Id == playerId) {
                    for(int i = 0 ; i < p.items.Count ; i++) {
                        if(p.items[i].id == item.id) {
                            p.items[i] = item;
                            break;
                        }
                    }
                }
            }
            await WriteFile(listofplayers);
        
            return null;
        }
        public async Task<Item> DeleteItem(Guid playerId, Item item){
            ListofPlayers listofplayers = await ReadFile();
            foreach(Player p in listofplayers.players) {
                if(p.Id == playerId) {
                    foreach(Item i in p.items) {
                        if(i.id == item.id){
                            p.items.Remove(i);
                            await WriteFile(listofplayers);
                        }
                    }
                }
            }
            return item;
        }

        public async Task<ListofPlayers> ReadFile() {
            ListofPlayers playerlist = new ListofPlayers();
            string path = @"game-dev.txt";
            await using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    string result = File.ReadAllText(path);
                    Console.WriteLine("mitäs tääl on: "+result);
                    if(result.Equals("")){
                        return playerlist;
                    }
                    else { playerlist.players = JsonConvert.DeserializeObject<List<Player>>(result); }
                }
            return playerlist;
        }

        public async Task<Player> WriteFile(ListofPlayers list) {
            string path = @"game-dev.txt";
            await using (FileStream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                stream.SetLength(0);
                string result = JsonConvert.SerializeObject(list.players);
                Console.WriteLine("Input: " + result);
                using var sr = new StreamWriter(stream);
                sr.Write(result);
                Console.WriteLine("Serialized " + result);
                stream.Flush();
            }
            return null;
        }

        public Task<Player[]> GetPlayerWithHigherScoreThan(int score){
            return null;
        }

        public Task<Player> GetPlayerWithName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetPlayersWithItemSize(int size)
        {
            throw new NotImplementedException();
        }

        public Task<Player> UpdatePlayerNameWithoutFetch(Guid playerId, string newName)
        {
            throw new NotImplementedException();
        }

        public Task<Player> IncrementPlayerWithoutFetch(Guid playerId, int incrementValue)
        {
            throw new NotImplementedException();
        }

        public Task<Player> PushItemToPlayer(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Player> RemoveItemAndIncrementScore(Guid playerId, Guid itemId, int score)
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> SortTop10PlayersDescending()
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> FindPlayersWithTag(string tag)
        {
            throw new NotImplementedException();
        }
    }
}