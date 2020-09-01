using System.Threading.Tasks;
using System;

public class OfflineCityBikeDataFetch : ICityBikeDataFetcher {
    public async Task<int> GetBikeCountInStation(string stationName) {
        try {
        string[] lines = await System.IO.File.ReadAllLinesAsync("bikedata.txt");
        for(int i = 0 ; i < lines.Length ; i++) {
            string[] splitted  = lines[i].Split(" : ");
            if(splitted[0].Equals(stationName)) {
                 return int.Parse(splitted[1]);
            }
        }
        throw new NotFoundException();
        } catch (ArgumentException e) {
            Console.WriteLine("That's not a name / " + e);
        } catch (NotFoundException e) {
            Console.WriteLine("Station " + stationName + " " + e);
        } 
        return 0;
    }
}