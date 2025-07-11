using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tripath.Models;

namespace tripath.Repositories
{
    public interface IUserDataDetailsRepository
    {
        Task CreateAsync(UserDataDetails data);
        Task UpdateUserTokenAsync(string userId, string? bearerToken);
    }
}
