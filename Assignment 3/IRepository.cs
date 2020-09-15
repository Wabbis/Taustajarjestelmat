using System.Threading.Tasks;
using System;
public interface IRepository
{
    Task<Player> Get(Guid id);
    Task<ListofPlayers> GetAll();
    Task<Player> Create(Player player);
    Task<Player> Modify(Guid id, ModifiedPlayer player);
    Task<Player> Delete(Guid id);
}