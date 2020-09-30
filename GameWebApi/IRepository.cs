using System;
using System.Threading.Tasks;

public interface IRepository
{        
        Task<Player> CreatePlayer(Player player);
        Task<Player> GetPlayer(Guid playerId);
        Task<Player> GetPlayer(string name);
        Task<Player[]> GetAllPlayers();
        Task<Player> UpdatePlayer(Player player);
        Task<Player> DeletePlayer(Guid playerId);

        Task<Item> CreateItem(Guid playerId, Item item);
        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> UpdateItem(Guid playerId, Item item);
        Task<Item> DeleteItem(Guid playerId, Item item);
        
        Task<Player[]> GetAllMinScore(int minScore);
        Task<Player[]> GetAllTag(string tag);
        Task<Player[]> GetAllItemType(ItemType type);
        Task<Player[]> GetAllItemCount(int count);
        Task<Player> Rename(Guid playerId, string name);
        Task<Player> IncrementPlayerScore(Guid playerId, int increment);
        Task<Player> SellItem(Guid playerId, Guid itemId, int score);
        Task<Player[]> GetTop10SortedByScoreDescending();
        Task<int> GetMostCommonLevel();
        Task<float> GetAvgScoreBetweenDates(DateTime a, DateTime b);
        Task<int> GetItemCountsForPrice(int price);
}