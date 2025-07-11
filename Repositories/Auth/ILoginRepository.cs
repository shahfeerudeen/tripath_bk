using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using tripath.Models;

namespace tripath.Repositories
{
    public interface IUserRepository
    {
        Task<(UserManagement?, UserMaster?)> GetUserWithMasterAsync(
            string username,
            string hashedPassword
        );
        Task UpdateUserTokenAsync(string userId, string? token);

        Task<UserManagement?> GetUserByUsernameAsync(string username);
        Task<UserMaster?> GetUserMasterByIdAsync(string? userMasterId);

        Task<(UserManagement?, UserMaster?)> GetUserWithMasterByUsernameOnlyAsync(string username);

        Task<(UserManagement, UserMaster?)> GetUserWithMasterByUserIdAsync(string userId);

        Task<UserManagement> GetByUserNameAsync(string userName);

        Task<bool> UpdateBearerTokenInUserDataDetailsAsync(string userId, string? token);

        Task<UserManagement?> GetPasswordByIdAsync(ObjectId id);
        Task<bool> ResetPasswordUpdateAsync(UserManagement user);
        Task<UserManagement?> GetUserByIdAsync(string userId);
    }
}
