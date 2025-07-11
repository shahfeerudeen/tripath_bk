using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tripath.Models;

namespace tripath .Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserManagement user);
    }
}