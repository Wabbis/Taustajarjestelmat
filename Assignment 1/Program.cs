using System;
using System.Threading.Tasks;

namespace Assignment_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if(args[0].Equals("offline") || args[0].Equals("Offline")){
                while(true) {
                OfflineCityBikeDataFetch fetch = new OfflineCityBikeDataFetch();
                Console.WriteLine("Anna haettavan aseman nimi: ");
                Console.WriteLine(await fetch.GetBikeCountInStation(Console.ReadLine()) + " pyörä(ä)");
            }}
            else if(args[0].Equals("Realtime") || args[0].Equals("realtime")) {
                while(true) {
                RealTimeCityBikeDataFetcher fetch = new RealTimeCityBikeDataFetcher();
                Console.WriteLine("Anna haettavan aseman nimi: ");
                Console.WriteLine(await fetch.GetBikeCountInStation(Console.ReadLine()) + " pyörä(ä)");
            }}
        }   
    }
}
