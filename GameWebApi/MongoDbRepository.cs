using System;
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

        public async Task<Player> GetPlayer(Guid id)
        {
            // var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
            // await _playerCollection.Find(filter).FirstAsync();
            var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            foreach (Player player in players)
            {
                if (player.Id == id) return player;
            }

            return null;
        }


        public async Task<Player> UpdatePlayer(Player player)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _playerCollection.ReplaceOneAsync(filter, player);
            return player;
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
    }

