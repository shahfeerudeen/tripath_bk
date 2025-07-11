using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;
using Microsoft.Extensions.Configuration;
using tripath.Services;
using System.Diagnostics.CodeAnalysis;

namespace tripath.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserManagement> _collection;
        private readonly IMongoCollection<UserMaster> _userMasterCollection;
        private readonly IMongoCollection<UserDataDetails> _userDataDetailsCollection;

        private readonly IFieldAliasService _alias;


        public UserRepository(IMongoClient mongoClient, IConfiguration config,IFieldAliasService alias)
        {
            var databaseName = config.GetSection("MongoDb")["DatabaseName"];
            if (string.IsNullOrEmpty(databaseName))
                throw new ArgumentNullException(nameof(databaseName), "MongoDb:DatabaseName is not configured properly.");

            var db = mongoClient.GetDatabase(databaseName);
            _collection = db.GetCollection<UserManagement>("UserManagement");

            _userMasterCollection = db.GetCollection<UserMaster>("UserMaster");
            _userDataDetailsCollection = db.GetCollection<UserDataDetails>("UserDataDetails");
              _alias = alias;
        }
        
     /*   public async Task<bool> UpdateBearerTokenInUserDataDetailsAsync(string userId, string? token)
        {

            var filter = Builders<UserDataDetails>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<UserDataDetails>
                .Update.Set(u => u.BearerToken, token)
                .Set(u => u.UserUpdateDate, DateTime.UtcNow);

            var result = await _userDataDetailsCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        } */
        //UpdateBearer Token Using alais
        public async Task<bool> UpdateBearerTokenInUserDataDetailsAsync(string userId, string? token)
            {
                // Get actual field names from alias mapping
                var userIdField = _alias.GetField("UserDataDetails", "UserId");
                var bearerTokenField = _alias.GetField("UserDataDetails", "BearerToken");
                var updateDateField = _alias.GetField("UserDataDetails", "UserUpdateDate");

                // Build the filter using the alias-based field name
                var filter = Builders<UserDataDetails>.Filter.Eq(userIdField, userId);

                // Build the update using alias-based field names
                var update = Builders<UserDataDetails>.Update
                    .Set(bearerTokenField, token)
                    .Set(updateDateField, DateTime.UtcNow);

                // Perform the update
                var result = await _userDataDetailsCollection.UpdateOneAsync(filter, update);
                return result.ModifiedCount > 0;
            }
           

        public async Task<(UserManagement?, UserMaster?)> GetUserWithMasterAsync(string username, string hashedPassword)
        {
            var user = await _collection
                .Find(x => x.UserName == username && x.UserPassword == hashedPassword && x.UserStatus == "Y")
                .FirstOrDefaultAsync();

            if (user == null)
                return (null, null);

            UserMaster? userMaster = null;

            if (!string.IsNullOrEmpty(user.UserMasterId) && ObjectId.TryParse(user.UserMasterId, out var masterObjectId))
            {
                var filter = Builders<UserMaster>.Filter.Eq("_id", masterObjectId);
                userMaster = await _userMasterCollection.Find(filter).FirstOrDefaultAsync();
            }

            return (user, userMaster);
        }

       //  New Logic using alias (only for UserName)
            public async Task<UserManagement?> GetUserByUsernameAsync(string username)
            {
                var userNameField = _alias.GetField("UserManagement", "UserName");

                var filter = Builders<UserManagement>.Filter.Eq(userNameField, username);

                return await _collection.Find(filter).FirstOrDefaultAsync();
            }
        /*This is my old logic GetUserByUsernameAsync
            // Old Logic without alias
            public async Task<UserManagement?> GetUserByUsernameWithoutAliasAsync(string username)
            {
                return await _collection
                    .Find(x => x.UserName == username)
                    .FirstOrDefaultAsync();
            }
        */


        /* public async Task<UserMaster?> GetUserMasterByIdAsync(string? userMasterId)
         {
             if (string.IsNullOrEmpty(userMasterId))
                 return null;

             if (ObjectId.TryParse(userMasterId, out var masterObjectId))
             {
                 var filter = Builders<UserMaster>.Filter.Eq("_id", masterObjectId);
                 return await _userMasterCollection.Find(filter).FirstOrDefaultAsync();
             }

             return null;
         } */

        //Using Alias name
        
        public async Task<UserMaster?> GetUserMasterByIdAsync(string? userMasterId)
            {
                if (string.IsNullOrEmpty(userMasterId))
                    return null;

                if (ObjectId.TryParse(userMasterId, out var masterObjectId))
                {
                    // Get mapped field name for alias "id" which returns "_id"
                    var idField = _alias.GetField("UserMaster", "id");

                    var filter = Builders<UserMaster>.Filter.Eq(idField, masterObjectId);
                    return await _userMasterCollection.Find(filter).FirstOrDefaultAsync();
                }

                return null;
            }


        /* public async Task<UserManagement> GetByUserNameAsync(string userName)
         {
             return await _collection.Find(x => x.UserName == userName).FirstOrDefaultAsync();
         } */

        //Using Alais Name
        public async Task<UserManagement> GetByUserNameAsync(string userName)
            {
                // Get the actual MongoDB field name for "UserName"
                var userNameField = _alias.GetField("UserManagement", "UserName");

                var filter = Builders<UserManagement>.Filter.Eq(userNameField, userName);
                return await _collection.Find(filter).FirstOrDefaultAsync();
            }


       public async Task UpdateUserTokenAsync(string userId, string? token)
        {
            var filter = Builders<UserDataDetails>.Filter.Eq(u => u.UserId, userId);

            var update = Builders<UserDataDetails>
                .Update.Set(u => u.BearerToken, string.IsNullOrEmpty(token) ? "-" : token)
                .Set(u => u.UserUpdateDate, DateTime.UtcNow);

            await _userDataDetailsCollection.UpdateOneAsync(filter, update);
        }

        /* public async Task<(UserManagement?, UserMaster?)> GetUserWithMasterByUsernameOnlyAsync(string username)
         {
             var user = await _collection.Find(x => x.UserName == username).FirstOrDefaultAsync();

             if (user == null)
                 return (null, null);

             UserMaster? userMaster = null;

             if (!string.IsNullOrWhiteSpace(user.UserMasterId))
             {
                 userMaster = await _userMasterCollection
                     .Find(x => x.UserMasterId == user.UserMasterId)
                     .FirstOrDefaultAsync();
             }

             return (user, userMaster);
         } */
        //Using Alias
     
       public async Task<(UserManagement?, UserMaster?)> GetUserWithMasterByUsernameOnlyAsync(string username)
        {
            // Get alias-mapped field names
            var userNameField = _alias.GetField("UserManagement", "UserName");
            var userMasterIdFieldInUser = _alias.GetField("UserManagement", "UserMasterId");
            var userMasterIdField = _alias.GetField("UserMaster", "UserMasterId"); // Usually "_id"

            // Find the user by username using alias-mapped field
            var userFilter = Builders<UserManagement>.Filter.Eq(userNameField, username);
            var user = await _collection.Find(userFilter).FirstOrDefaultAsync();

            if (user == null)
                return (null, null);

            UserMaster? userMaster = null;

            if (!string.IsNullOrWhiteSpace(user.UserMasterId))
            {
                object masterId;

                // Convert string ID to ObjectId if possible
                if (ObjectId.TryParse(user.UserMasterId, out var objectId))
                    masterId = objectId;
                else
                    masterId = user.UserMasterId;

                // Build filter using alias-mapped field
                var masterFilter = Builders<UserMaster>.Filter.Eq(userMasterIdField, masterId);

                // Query the UserMaster collection
                userMaster = await _userMasterCollection.Find(masterFilter).FirstOrDefaultAsync();
            }

            return (user, userMaster);
        }

        public async Task<(UserManagement?, UserMaster?)> GetUserWithMasterByUserIdAsync(string userId)
        {
            var user = await _collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (user == null)
                 return (null, null); ;

            var userMaster = await _userMasterCollection
                .Find(x => x.UserMasterId == user.UserMasterId)
                .FirstOrDefaultAsync();

            return (user, userMaster);
        }

        public async Task<UserManagement?> GetPasswordByIdAsync(ObjectId id)
        {
            var filter = Builders<UserManagement>.Filter.Eq(u => u.UserId, id.ToString());
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> ResetPasswordUpdateAsync(UserManagement user)
        {
            var filter = Builders<UserManagement>.Filter.Eq(u => u.UserId, user.UserId);
            var result = await _collection.ReplaceOneAsync(filter, user);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<UserManagement?> GetUserByIdAsync(string userId)
        {
            return await _collection
                .Find(user => user.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
