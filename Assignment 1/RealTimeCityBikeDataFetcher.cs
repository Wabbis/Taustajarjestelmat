using System.Net.Http;
using System;
using System.Threading.Tasks;

public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher {
    public async Task<int> GetBikeCountInStation(string stationName) {
        HttpClient client = new HttpClient();
        try {
        string response = await client.GetStringAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
        BikeRentalStationList list = Newtonsoft.Json.JsonConvert.DeserializeObject<BikeRentalStationList>(response);
        for(int i = 0 ; i < list.stations.Length; i++ ) {
        if(list.stations[i].name.Equals(stationName)) {
            return list.stations[i].bikesAvailable;
            }
        }
        throw new NotFoundException();
        } catch (ArgumentException e) {
            Console.WriteLine("That's not a name / " + e);
        } catch (NotFoundException e) {
            Console.WriteLine("Station " + stationName + " " +e);
        } 

            
    return 0; 
    }
}
