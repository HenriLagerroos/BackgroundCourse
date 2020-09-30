using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
//using game_server.Players;
using MongoDB.Driver;

    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("game");
            _playerCollection = database.GetCollection<Player>("players");

            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _playerCollection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player[]> GetAllPlayers()
        {
            var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player[]> GetAllMinScore(int minScore)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Gte(p => p.Score, minScore);
            var players = await _playerCollection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        
        public async Task<Player[]> GetAllTag(string tag)
        {
            var filter = Builders<Player>.Filter.AnyEq(p => p.Tags, tag);

            var players = await _playerCollection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player[]> GetAllItemType(ItemType type)
        {
            var playersWithWeapons =
                Builders<Player>.Filter.ElemMatch<Item>(
                    p => p.Items,
                    Builders<Item>.Filter.Eq(
                        i => i.Type,
                        type
                    )
                );

            var players = await _playerCollection.Find(playersWithWeapons).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player[]> GetAllItemCount(int count)
        {
             var filter = Builders<Player>.Filter.Size(p => p.Items, count);

             var players = await _playerCollection.Find(filter).ToListAsync();
             return players.ToArray();
        }

        public Task<Player> GetPlayer(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
            return _playerCollection.Find(filter).FirstAsync();
        }

        public Task<Player> GetPlayer(string name)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Name, name);
            return _playerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _playerCollection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player> IncrementPlayerScore(Guid playerId, int increment)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var incrementScoreUpdate = Builders<Player>.Update.Inc(p => p.Score, increment);
            var options = new FindOneAndUpdateOptions<Player>()
            {
                ReturnDocument = ReturnDocument.After
            };
            Player player = await _playerCollection.FindOneAndUpdateAsync(filter, incrementScoreUpdate, options);
            return player;
        }

        public Task<Player> SellItem(Guid playerId, Guid itemId, int score)
        {

            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);

            var pull = Builders<Player>.Update.PullFilter(p => p.Items, i => i.Id == itemId);
            var inc = Builders<Player>.Update.Inc(p => p.Score, score);
            var update = Builders<Player>.Update.Combine(pull, inc);

            var options = new FindOneAndUpdateOptions<Player>()
            {
                ReturnDocument = ReturnDocument.After
            };

            return _playerCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<Player> Rename(Guid playerId, string name)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var options = new FindOneAndUpdateOptions<Player>()
            {
                ReturnDocument = ReturnDocument.After
            };
             return await _playerCollection.FindOneAndUpdateAsync(filter, Builders<Player>.Update.Set("Name", name), options);
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            return await _playerCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);

            var update = Builders<Player>.Update.Push(e => e.Items, item);

            await _playerCollection.FindOneAndUpdateAsync(filter, update);

            return item;
        }

        public async Task<Player[]> GetTop10SortedByScoreDescending()
        {
            var sortDef = Builders<Player>.Sort.Descending(p => p.Score);
            var players = await _playerCollection.Find(new BsonDocument()).Limit(10).Sort(sortDef).ToListAsync();

            return players.ToArray();
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();

            foreach (Item item in player.Items)
            {
                if (item.Id == itemId)
                {
                    return item;
                }
            }
            return null;
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();

            return player.Items.ToArray();
        }

        public async Task<Item> UpdateItem(Guid playerId, Item updatedItem)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();

            for (int i = 0; i < player.Items.Count; ++i)
            {
                if (player.Items[i].Id == updatedItem.Id)
                {
                    player.Items[i] = updatedItem;
                    await _playerCollection.ReplaceOneAsync(filter, player);
                    return player.Items[i];
                }
            }

            return null;
        }
        

        public async Task<Item> DeleteItem(Guid playerId, Item deleteItem)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();

            foreach (Item item in player.Items)
            {
                if (item.Id == deleteItem.Id)
                {
                    Item deletedItem = item;
                    player.Items.Remove(item);
                    await _playerCollection.ReplaceOneAsync(filter, player);
                    return deletedItem;
                }
            }

            return null;
        }

        public async Task<int> GetMostCommonLevel()
        {
            var query = _playerCollection.AsQueryable()
                .GroupBy(p => p.Level)
                .Select(p => new LevelCount { Id = p.Key, Count = p.Count() })
                .OrderByDescending(p => p.Count)
                .First();
            return query.Count;
        }
        public async Task<float> GetAvgScoreBetweenDates(DateTime a, DateTime b)
        {
            var query = _playerCollection.AsQueryable()
                .Where(p => a <= p.CreationTime && p.CreationTime <= b)
                .Select(p => p.Score)
                .Average(p => (float)p);
            return query;
        }

        public async Task<int> GetItemCountsForPrice(int price)
        {

            var query = _playerCollection.AsQueryable()
                .SelectMany(p => p.Items)
                .Where(p => p.Level == price)
                .Count();
            return query;
        }
}

