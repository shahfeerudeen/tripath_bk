using MongoDB.Driver;

namespace Data
{
    public class DbContextClass
    {
        private readonly IMongoDatabase _database;

        public DbContextClass(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoDatabase Database => _database;
    }
}
