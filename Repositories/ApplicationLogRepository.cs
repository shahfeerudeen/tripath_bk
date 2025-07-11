using MongoDB.Driver;
using System.Threading.Tasks;
using tripath.Services;
namespace tripath.Repositories
{
    public class ApplicationLogRepository : IApplicationLogRepository
    {
        private readonly IMongoCollection<ApplicationLog> _collection;
        private readonly IFieldAliasService _alias;

        public ApplicationLogRepository(IMongoDatabase database,IFieldAliasService alias)
        {
            _collection = database.GetCollection<ApplicationLog>("ApplicationLogs");
            _alias = alias;
        }

        /* public async Task LogAsync(string action, string userId)
         {
             var log = new ApplicationLog
             {
                 ApplicationAction = action,
                 UserId = userId
             };

             await _collection.InsertOneAsync(log);
         } */
         public async Task LogAsync(string action, string userId)
        {
            // Fetch alias fields (for future use if needed in filters/updates)
            var actionField = _alias.GetField("ApplicationLogs", "ApplicationAction");
            var userIdField = _alias.GetField("ApplicationLogs", "UserId");
            var entryDateField = _alias.GetField("ApplicationLogs", "ApplicationLogEntryDate");

            // Preserve old model-based insert logic
            var log = new ApplicationLog
            {
                ApplicationAction = action,
                UserId = userId,
                ApplicationLogEntryDate = DateTime.UtcNow
            };

            await _collection.InsertOneAsync(log);
        }

        public async Task LogAsync(string action)
        {
            await LogAsync(action, "Anonymous");
        }
    }
}