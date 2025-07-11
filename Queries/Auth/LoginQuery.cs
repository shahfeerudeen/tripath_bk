using System;
using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class LoginQuery : IRequest<UserManagementResponse>
    {
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public string? OrganizationId { get; set; } // Use Id instead of Name

        public LoginQuery() { }

        public LoginQuery(string username, string password, string organizationId)
        {
            UserName = username?.Trim();
            UserPassword = password?.Trim();
            OrganizationId = OrganizationId?.Trim();
        }
    }
}
