using System.Threading.Tasks;
using MongoDB.Driver;
using tripath.Models;
using Microsoft.Extensions.Configuration;
using tripath.Services;
using MongoDB.Bson;

namespace tripath.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly IMongoCollection<OtpVerification> _collection;
        private readonly IFieldAliasService _alias;

        public OtpRepository(IMongoClient client, IConfiguration config, IFieldAliasService alias)
        {
            var databaseName = config.GetValue<string>("MongoDb:DatabaseName");
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<OtpVerification>("OtpVerifications");
            _alias = alias;
        }

        public async Task SaveOtpAsync(OtpVerification otp)
        {
            await _collection.InsertOneAsync(otp);
        }

        /*   public async Task<OtpVerification?> GetOtpByUserIdOtpAndTypeAsync(string userId, string otp, string otpType)
           {
               var filter = Builders<OtpVerification>.Filter.Eq(x => x.UserId, userId);
               var records = await _collection.Find(filter)
                                           .SortByDescending(x => x.OtpEntryDate)
                                           .ToListAsync();

               foreach (var record in records)
               {
                   if (record.OtpType == "both")
                   {
                       var parts = record.Otp.Split('|');
                       if (otpType == "email" && parts.Length > 0 && parts[0] == otp)
                           return record;

                       if (otpType == "phone" && parts.Length > 1 && parts[1] == otp)
                           return record;
                   }
                   else if (record.OtpType == otpType && record.Otp == otp)
                   {
                       return record;
                   }
               }

               return null;
           } */
        //Using alais
        public async Task<OtpVerification?> GetOtpByUserIdOtpAndTypeAsync(string userId, string otp, string otpType)
        {
            // Get field names from alias service
            var userIdField = _alias.GetField("OtpVerifications", "UserId").Trim();
            var entryDateField = _alias.GetField("OtpVerifications", "OtpEntryDate").Trim();

            // Build filter based on alias-based field
            var filter = Builders<OtpVerification>.Filter.Eq(userIdField, userId);

            // Sort by latest OTP entry date
            var sort = Builders<OtpVerification>.Sort.Descending(entryDateField);

            // Fetch all OTP records for user
            var records = await _collection.Find(filter).Sort(sort).ToListAsync();

            // Match OTP logic
            foreach (var record in records)
            {
                if (record.OtpType == "both")
                {
                    var parts = record.Otp?.Split('|');
                    if (otpType == "email" && parts?.Length > 0 && parts[0] == otp)
                        return record;

                    if (otpType == "phone" && parts?.Length > 1 && parts[1] == otp)
                        return record;
                }
                else if (record.OtpType == otpType && record.Otp == otp)
                {
                    return record;
                }
            }

            return null;
        }

        /* public async Task<OtpVerification?> GetLatestOtpAsync(string userId)
         {
             return await _collection
                 .Find(x => x.UserId == userId)
                 .SortByDescending(x => x.OtpEntryDate)
                 .FirstOrDefaultAsync();
         }*/
        public async Task<OtpVerification?> GetLatestOtpAsync(string userId)
        {
            var userIdField = _alias.GetField("OtpVerifications", "UserId").Trim();
            var entryDateField = _alias.GetField("OtpVerifications", "OtpEntryDate").Trim();

            var filter = Builders<OtpVerification>.Filter.Eq(userIdField, userId);
            var sort = Builders<OtpVerification>.Sort.Descending(entryDateField);

            return await _collection.Find(filter).Sort(sort).FirstOrDefaultAsync();
        }


        /* public async Task IncrementOtpAttemptAsync(string otpId)
         {
             var filter = Builders<OtpVerification>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(otpId));
             var update = Builders<OtpVerification>.Update.Inc(x => x.AttemptCount, 1);
             await _collection.UpdateOneAsync(filter, update);
         }*/
        public async Task IncrementOtpAttemptAsync(string otpId)
        {
            var idField = _alias.GetField("OtpVerifications", "Id");
            var attemptCountField = _alias.GetField("OtpVerifications", "AttemptCount");

            var filter = Builders<OtpVerification>.Filter.Eq(idField, ObjectId.Parse(otpId));
            var update = Builders<OtpVerification>.Update.Inc(attemptCountField, 1);

            await _collection.UpdateOneAsync(filter, update);
        }


        /* public async Task<OtpVerification?> GetOtpByValueAsync(string otp)
         {
             return await _collection.Find(x => x.Otp == otp).FirstOrDefaultAsync();
         } */
        public async Task<OtpVerification?> GetOtpByValueAsync(string otp)
        {
            var otpField = _alias.GetField("OtpVerifications", "Otp");

            var filter = Builders<OtpVerification>.Filter.Eq(otpField, otp);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }


        /*   public async Task<OtpVerification?> GetLatestOtpByUserIdAndOtpAsync(string userId, string otp)
           {
               var filter = Builders<OtpVerification>.Filter.And(
                   Builders<OtpVerification>.Filter.Eq(x => x.UserId, userId),
                   Builders<OtpVerification>.Filter.Eq(x => x.Otp, otp)
               );

               return await _collection
                   .Find(filter)
                   .SortByDescending(x => x.OtpEntryDate)
                   .FirstOrDefaultAsync();
           }*/
        public async Task<OtpVerification?> GetLatestOtpByUserIdAndOtpAsync(string userId, string otp)
        {
            var userIdField = _alias.GetField("OtpVerifications", "UserId");
            var otpField = _alias.GetField("OtpVerifications", "Otp");
            var entryDateField = _alias.GetField("OtpVerifications", "OtpEntryDate");

            var filter = Builders<OtpVerification>.Filter.And(
                Builders<OtpVerification>.Filter.Eq(userIdField, userId),
                Builders<OtpVerification>.Filter.Eq(otpField, otp)
            );

            //  var entryDateField = _alias.GetField("OtpVerifications", "OtpEntryDate");

            return await _collection.Find(filter)
                .Sort(Builders<OtpVerification>.Sort.Descending(entryDateField))
                .FirstOrDefaultAsync();

        }


        /*   public async Task<OtpVerification?> GetLatestOtpByUserIdTypeAndOtpAsync(string userId, string otpType, string otp)
           {
               var filter = Builders<OtpVerification>.Filter.And(
                   Builders<OtpVerification>.Filter.Eq(x => x.UserId, userId),
                   Builders<OtpVerification>.Filter.Eq(x => x.OtpType, otpType),
                   Builders<OtpVerification>.Filter.Eq(x => x.Otp, otp)
               );

               return await _collection
                   .Find(filter)
                   .SortByDescending(x => x.OtpEntryDate)
                   .FirstOrDefaultAsync();
           } */
        public async Task<OtpVerification?> GetLatestOtpByUserIdTypeAndOtpAsync(string userId, string otpType, string otp)
        {
            var userIdField = _alias.GetField("OtpVerifications", "UserId");
            var otpField = _alias.GetField("OtpVerifications", "Otp");
            var otpTypeField = _alias.GetField("OtpVerifications", "OtpType");
            var entryDateField = _alias.GetField("OtpVerifications", "OtpEntryDate");

            var filter = Builders<OtpVerification>.Filter.And(
                Builders<OtpVerification>.Filter.Eq(userIdField, userId),
                Builders<OtpVerification>.Filter.Eq(otpTypeField, otpType),
                Builders<OtpVerification>.Filter.Eq(otpField, otp)
            );

            return await _collection.Find(filter)
                        .Sort(Builders<OtpVerification>.Sort.Descending(entryDateField))
                        .FirstOrDefaultAsync();

        }


        /* public async Task<OtpVerification?> GetLatestOtpByUserIdAndTypeAsync(string userId, string otpType)
         {
             var filter = Builders<OtpVerification>.Filter.And(
                 Builders<OtpVerification>.Filter.Eq(x => x.UserId, userId),
                 Builders<OtpVerification>.Filter.Eq(x => x.OtpType, otpType)
             );

             return await _collection
                 .Find(filter)
                 .SortByDescending(x => x.OtpEntryDate)
                 .FirstOrDefaultAsync(); */
        public async Task<OtpVerification?> GetLatestOtpByUserIdAndTypeAsync(string userId, string otpType)
        {
            var userIdField = _alias.GetField("OtpVerifications", "UserId");
            var otpTypeField = _alias.GetField("OtpVerifications", "OtpType");
            var entryDateField = _alias.GetField("OtpVerifications", "OtpEntryDate");

            var filter = Builders<OtpVerification>.Filter.And(
                Builders<OtpVerification>.Filter.Eq(userIdField, userId),
                Builders<OtpVerification>.Filter.Eq(otpTypeField, otpType)
            );

            return await _collection.Find(filter)
                      .Sort(Builders<OtpVerification>.Sort.Descending(entryDateField))
                      .FirstOrDefaultAsync();
        }


            //This is for Session Mangement without using alias

                public async Task<bool> UpdateOtpSessionAsync(string otpId, string sessionId, bool isVerified, DateTime sessionExpiry)
        {
            var idField = _alias.GetField("OtpVerifications", "Id");
            var sessionIdField = _alias.GetField("OtpVerifications", "SessionId");
            var isVerifiedField = _alias.GetField("OtpVerifications", "IsOtpVerified");
            var expiryField = _alias.GetField("OtpVerifications", "SessionExpiry");
            var updateDateField = _alias.GetField("OtpVerifications", "OtpUpdateDate");

            var filter = Builders<OtpVerification>.Filter.Eq(idField, ObjectId.Parse(otpId));

            var update = Builders<OtpVerification>.Update
                .Set(sessionIdField, sessionId)
                .Set(isVerifiedField, isVerified)
                .Set(expiryField, sessionExpiry)
                .Set(updateDateField, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<OtpVerification?> GetOtpBySessionIdAsync(string sessionId)
        {
            var sessionField = _alias.GetField("OtpVerifications", "SessionId");
            var filter = Builders<OtpVerification>.Filter.Eq(sessionField, sessionId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    


            }

        }

