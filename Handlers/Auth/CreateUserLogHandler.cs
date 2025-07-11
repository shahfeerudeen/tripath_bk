using System.Threading;
using System.Threading.Tasks;
using MediatR;
using tripath.Commands;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class CreateUserLogHandler : IRequestHandler<CreateUserLogCommand, UserLog>
    {
        private readonly IUserLogRepository _repository;

        public CreateUserLogHandler(IUserLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserLog> Handle(
            CreateUserLogCommand request,
            CancellationToken cancellationToken
        )
        {
            var log = new UserLog
            {
                UserId = request.UserId,
                IPAddress = request.IPAddress,
                UserLogStatus = request.Status,
            };

            return await _repository.CreateAsync(log);
        }
    }
}
