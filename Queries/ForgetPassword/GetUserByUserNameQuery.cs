using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetUserByUserNameQuery : IRequest<UserManagement>
    {
        public string UserName { get; set; }

        public GetUserByUserNameQuery(string userName)
        {
            UserName = userName.Trim();
        }
    }
}
