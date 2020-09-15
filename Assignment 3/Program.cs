using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FileRepos;


namespace Assignment_3
{
    public class Program
    {
        public static void Main(string[] args)
        {

/*             
            FileRepository filerepo = new FileRepository();

            Player player = new Player();
            player.CreationTime = DateTime.Now;
            player.Name = "Matti";
            player.Score = 200;
            player.Level = 15; 
            player.IsBanned = false;
            player.Id = Guid.NewGuid(); 
            
            var task = filerepo.Create(player);
            task.Wait();

            

            Player player1 = new Player();
            player1.CreationTime = DateTime.Now;
            player1.Name = "Paras";
            player1.Score = 15000;
            player1.Level = 60; 
            player1.IsBanned = true;
            player1.Id = Guid.NewGuid();

            task = filerepo.Create(player1); 
            task.Wait();
            Task<ListofPlayers> allplayers = filerepo.GetAll();
            allplayers.Wait();
            ListofPlayers list = new ListofPlayers();
            list.players = allplayers.Result.players.ToList();
            Console.WriteLine(list.players.Count);
            foreach (Player p in list.players) 
            {
                Console.WriteLine(p.Name + ", " + p.Id + ", " + p.Level + ", " + p.Score + ", " + p.IsBanned);
            } */
        CreateHostBuilder(args).Build().Run();
    }
    


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

