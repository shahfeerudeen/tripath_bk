using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tripath.Models;

namespace tripath.Commands.Auth
{
     public class LogoutCommand : IRequest<UserLog>
    {
        
    }
}