using System.Threading.Tasks;
using System;
public interface IRepository
{
    Task<Player> GetPlayer(Guid playerId);
    Task<Player[]> GetAll();
    Task<Player> Create(Player player);
    Task<Player> UpdatePlayer(Player player);
    Task<Player> DeletePlayer(Guid playerId);
    Task<Item> CreateItem(Guid playerId, Item item);
    Task<Item> GetItem(Guid playerId, Guid itemId);
    Task<Item[]> GetAllItems(Guid playerId);
    Task<Item> UpdateItem(Guid playerId, Item item);
    Task<Item> DeleteItem(Guid playerId, Item item);
    Task<Player[]> GetPlayerWithHigherScoreThan(int score);
    Task<Player> GetPlayerWithName(string name);
    Task<Player[]> GetPlayersWithItemSize(int size);
    Task<Player> UpdatePlayerNameWithoutFetch(Guid playerId, string newName);
    Task<Player> IncrementPlayerWithoutFetch(Guid playerId, int incrementValue);
    Task<Player> PushItemToPlayer(Guid playerId, Item item);
    Task<Player> RemoveItemAndIncrementScore(Guid playerId, Guid itemId, int score);
    Task<Player[]> SortTop10PlayersDescending();
    Task<Player[]> FindPlayersWithTag(string tag);
}