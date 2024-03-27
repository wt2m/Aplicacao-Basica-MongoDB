using ConsoleApp1.Configurations;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
        await _collection.ReplaceOneAsync(filter, entity);
    }
}