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
        public async Task<Player> Get(Guid id){
            ListofPlayers List = await GetAll();
            foreach(Player p in List.players)  {
                if(p.Id == id) {
                    return p;
                }
            } 
           return null;

        }
        public async Task<ListofPlayers> GetAll()
        {
            ListofPlayers playerlist = new ListofPlayers();
            string path = @"game-dev.txt";
            await using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    string result = File.ReadAllText(path);
                    
                    if(result.Equals("")){
                        return playerlist;
                    }
                    else{ playerlist.players = JsonConvert.DeserializeObject<List<Player>>(result); }
                }
            Console.WriteLine("Players in: " + playerlist.players.Count);
            
            return playerlist;
        }

        public async Task<Player> Create(Player player) {
            string path = @"game-dev.txt";
            ListofPlayers allPlayers = await GetAll();          
            allPlayers.players.Add(player);
            await using (FileStream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                string result = JsonConvert.SerializeObject(allPlayers.players);
                Console.WriteLine("Input: " + result);
                using var sr = new StreamWriter(stream);
                sr.WriteLine(result);
                Console.WriteLine("Serialized");

            }
            return player;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player){
            ListofPlayers List = await GetAll();
            foreach(Player p in List.players)  {
                if(p.Id == id){
                    p.Score = player.Score;
                }
            }
            return null; 
        }
        public async Task<Player> Delete(Guid id){
            ListofPlayers List = await GetAll();
            foreach(Player p in List.players)  {
                if (p.Id == id) { 
                }
            }
            return null;
        }
    }
}