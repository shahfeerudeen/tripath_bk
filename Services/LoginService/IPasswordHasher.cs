using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tripath .Services
{
    public interface IPasswordHasher
    {
        string HashWithMD5(string input);
    }
}