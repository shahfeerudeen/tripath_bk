using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tripath.ApiResponse;

namespace tripath.Commands
{
    public class ForgetPasswordCommand : IRequest<object>
    {
        public string? UserName { get; set; }
    }
}
