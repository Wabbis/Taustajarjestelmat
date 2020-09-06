using System;
using System.Linq;

namespace ExtensionMethods {
    public static class Extensions {
        public static Item GetHighestLevelItem(this Player player) {
            Item highestItem = null;
            foreach (Item i in player.items) {
                if(highestItem == null) {
                    highestItem = i;
                }
                else if(i.level > highestItem.level){
                    highestItem = i;
                }
            }
            return highestItem;
        }

        public static void ListItems(this Player player) {
            foreach (Item i in player.items) {
                Console.WriteLine(i.level);
            }
        }
        public static Item[] GetItems(this Player player) {
            Item[] arr = new Item[player.items.Count];
            int x = 0;
            foreach (Item i in player.items) {
                arr[x] = i; 
                x++;
            }
            return arr;
        }

        public static Item[] GetItemsWithLinq(this Player player) {
            return player.items.ToArray();
        }

        public static Item FirstItem(this Player player) {
            return player.items[0];
        }
        
        public static Item FirstItemWithLinq(this Player player) {
            return player.items.First();
        }

        
    }
}