using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tripath.Models;

namespace tripath.Repositories
{
    public interface IUserLogRepository
    {
        Task<UserLog> CreateAsync(UserLog log);
        Task<UserLog> UpdateStatusAsync(string userId, string status);
        
    }
}