using System;
using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class LoginCommand : IRequest<UserManagementResponse>
    {
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public string? OrganizationId { get; set; }

        public LoginCommand() { }

        public LoginCommand(string username, string password, string organizationId)
        {
            UserName = username?.Trim();
            UserPassword = password?.Trim();
            OrganizationId = organizationId?.Trim(); // Fixed bug here
        }
    }
}
