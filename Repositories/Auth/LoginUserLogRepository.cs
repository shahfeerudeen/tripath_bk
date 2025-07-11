using MongoDB.Driver;
using tripath.Models;
using System.Threading.Tasks;
using tripath.Services;

namespace tripath.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly IMongoCollection<UserLog> _collection;
             private readonly IFieldAliasService _alias;

        public UserLogRepository(IMongoDatabase database,IFieldAliasService alias)
        {
            _collection = database.GetCollection<UserLog>("UserLog");

            _alias = alias;
        }

        public async Task<UserLog> CreateAsync(UserLog log)
        {
            log.UserLogEntryDate = DateTime.UtcNow;
            log.UserLogUpdateDate = DateTime.UtcNow;

            await _collection.InsertOneAsync(log);
            return log;
        }

       
  public async Task<UserLog?> UpdateStatusAsync(string userId, string status)
{
    // Get alias field names
    var userIdField = _alias.GetField("UserLog", "UserId");
    var statusField = _alias.GetField("UserLog", "UserLogStatus");
    var updateDateField = _alias.GetField("UserLog", "UserLogUpdateDate");
    var logoutTimeField = _alias.GetField("UserLog", "UserLogoutTime");

    // Build filter using alias fields
    var filter = Builders<UserLog>.Filter.And(
        Builders<UserLog>.Filter.Eq(userIdField, userId),
        Builders<UserLog>.Filter.Eq(statusField, "I") // Only update active login
    );

    // Build update using alias fields
    var update = Builders<UserLog>.Update
        .Set(statusField, status)
        .Set(updateDateField, DateTime.UtcNow)
        .Set(logoutTimeField, DateTime.UtcNow);

    var options = new FindOneAndUpdateOptions<UserLog>
    {
        ReturnDocument = ReturnDocument.After
    };

    // Execute update
    return await _collection.FindOneAndUpdateAsync(filter, update, options);
}


    }
}