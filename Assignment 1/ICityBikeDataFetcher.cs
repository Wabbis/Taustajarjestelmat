using System.Threading.Tasks;

public interface ICityBikeDataFetcher
{
    public Task<int> GetBikeCountInStation(string stationName);
}