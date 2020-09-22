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
}