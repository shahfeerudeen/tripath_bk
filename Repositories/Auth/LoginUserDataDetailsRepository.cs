using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class UserDataDetailsRepository : IUserDataDetailsRepository
    {
        private readonly IMongoCollection<UserDataDetails> _collection;

        public UserDataDetailsRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<UserDataDetails>("UserDataDetails");
        }

        public async Task CreateAsync(UserDataDetails data)
        {
            await _collection.InsertOneAsync(data);
        }
        public async Task UpdateUserTokenAsync(string userId, string? bearerToken)
        {
            var filter = Builders<UserDataDetails>.Filter.Eq(x => x.UserId, userId);
            var update = Builders<UserDataDetails>.Update.Set(x => x.BearerToken, bearerToken);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}