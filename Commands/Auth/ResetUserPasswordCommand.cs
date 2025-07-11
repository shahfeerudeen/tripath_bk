using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;

namespace tripath.Commands.Auth
{
    public class ResetUserPasswordCommand : IRequest<bool>
    {

        public string? UserId { get; set; }
        public string? NewPassword { get; set; }
        public string? UserName { get; set; }
        public string? SessionId { get; set; }

    }   
}