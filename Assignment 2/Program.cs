using System;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;

namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            Boolean tempcheck = true;
            Guid temp = Guid.Empty;
            List<Player> playerlist = new List<Player>();
            // Luodaan 50 normaalia pelaajaa
            for(int i = 0 ; i < 50 ; i++) {
                Boolean GUIDNotConfirmed = true;
                while(GUIDNotConfirmed) {
                    temp = Guid.NewGuid();
                    for(int j = 0 ; j < playerlist.Count ; j++) {
                        tempcheck = true;
                        if(playerlist[j].id == temp ) {
                            tempcheck = false;
                            break;
                        }
                    }
                    if (tempcheck) {
                        GUIDNotConfirmed = false; 
                    }
                }
                Player player = new Player();
                player.id = temp;
                player.score = r.Next(0,1000);
                playerlist.Add(player);
            }



            List<PlayerForAnotherGame> wrongplayerlist = new List<PlayerForAnotherGame>();
            // Luodaan 50 epänormaalia pelaajaa
            for(int i = 0 ; i < 50 ; i++) {
                Boolean GUIDNotConfirmed = true;
                while(GUIDNotConfirmed) {
                    temp = Guid.NewGuid();
                    for(int j = 0 ; j < wrongplayerlist.Count ; j++) {
                        tempcheck = true;
                        if(wrongplayerlist[j].id == temp ) {
                            tempcheck = false;
                            break;
                        }
                    }
                    if (tempcheck) {
                        GUIDNotConfirmed = false; 
                    }
                }
                PlayerForAnotherGame player = new PlayerForAnotherGame();
                player.id = temp;
                player.score = r.Next(0,1000);
                wrongplayerlist.Add(player);
            }

            Game<Player> normalGame = new Game<Player>(playerlist);
            Game<PlayerForAnotherGame> unnormalGame = new Game<PlayerForAnotherGame>(wrongplayerlist);

     

            Player[] top10 = normalGame.GetTop10Players();
            PlayerForAnotherGame[] topTen = unnormalGame.GetTop10Players();

            foreach(Player p in top10) {
                Console.WriteLine(p.id + ", Score: " + p.score);
            }

            foreach(PlayerForAnotherGame p in topTen) {
                Console.WriteLine(p.id + ", Score: " + p.score);
            }



    /*
            Guid g = Guid.NewGuid();
            Player aatami = new Player();
            
            Tehtävä 3 ja 4 Item Harjoitukset.    
            Itemien luonti
            for (int i = 0 ; i < 10 ; i++)
            {
                
                g = Guid.NewGuid();
                Random r = new Random();
                Item miekka = new Item(g, r.Next(100, 500));
                aatami.items.Add(miekka);
            } */
    /*      Item[] arr = aatami.GetItemsWithLinq();
            foreach (Item i in arr) {
                Console.WriteLine(i.level.ToString());
            } */ 


/*          Tehtävä 5 ja 6   
            Action<Item> x = PrintItem;
            aatami.items.ToList().ForEach(n => x(n));
            ProcessEachItem(aatami, x); */

            // Console.WriteLine("Highest itemlevel: " + aatami.GetHighestLevelItem().level);
        }

        static void PrintItem(Item item) {
            Console.WriteLine(item.id);
            Console.WriteLine(item.level);
        }

        static void ProcessEachItem(Player player, Action<Item> process) {
            foreach (Item i in player.items) {
                process(i);
            }
        }
    }
}


