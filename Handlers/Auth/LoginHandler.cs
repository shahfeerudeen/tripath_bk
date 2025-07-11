using MediatR;
using MongoDB.Bson;
using tripath.Commands;
using tripath.Models;
using tripath.Repositories;
using tripath.Services;
using tripath.Utils;

namespace tripath.Handlers.Auth
{
    public class LoginHandler : IRequestHandler<LoginCommand, UserManagementResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IUserDataDetailsRepository _userDataDetailsRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public LoginHandler(
            IUserRepository repository,
            IJwtTokenService tokenService,
            IPasswordHasher passwordHasher,
            IUserLogRepository userLogRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserDataDetailsRepository userDataDetailsRepository,
            IOrganizationRepository organizationRepository
        )
        {
            _repository = repository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _userLogRepository = userLogRepository;
            _httpContextAccessor = httpContextAccessor;
            _userDataDetailsRepository = userDataDetailsRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<UserManagementResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var username = request.UserName?.Trim();
                var password = request.UserPassword?.Trim();
                var organizationId = request.OrganizationId?.Trim();

                // Validate required fields dynamically
                var missingFields = new List<string>();
                if (string.IsNullOrWhiteSpace(username)) missingFields.Add($"{AppStrings.Constants.UserName}");
                if (string.IsNullOrWhiteSpace(password)) missingFields.Add($"{AppStrings.Constants.Password}");
                if (string.IsNullOrWhiteSpace(organizationId)) missingFields.Add($"{AppStrings.Constants.OrganisationId}");

                if (missingFields.Count > 0)
                {
                    string validationMessage = missingFields.Count switch
                    {
                        1 => $"{missingFields[0]} {AppStrings.Messages.isRequired}",
                        2 => $"{missingFields[0]} {AppStrings.Constants.and} {missingFields[1]} {AppStrings.Messages.areRequired}",
                        _ => $"{string.Join(", ", missingFields.Take(missingFields.Count - 1))}, {AppStrings.Constants.and} {missingFields.Last()} {AppStrings.Messages.areRequired}"
                    };
                    throw new ArgumentException(validationMessage);
                }

                //  Validate ObjectId
                if (!ObjectId.TryParse(organizationId, out var orgObjectId))
                    throw new ArgumentException(AppStrings.Messages.InvalidOrgId);

                //  Get organization
                var organization = await _organizationRepository.GetByIdAsync(organizationId!)
                    ?? throw new UnauthorizedAccessException(AppStrings.Messages.OrganizationNotFound);

                // Get user
                var (user, userMaster) = await _repository.GetUserWithMasterByUsernameOnlyAsync(username!);
                if (user == null)
                    throw new UnauthorizedAccessException(AppStrings.Messages.UserNotFound);

                if (user.UserStatus != AppStrings.Status.Y)
                    throw new UnauthorizedAccessException(AppStrings.Messages.UserNotActive);

                // Password verification
                var hashedPassword = _passwordHasher.HashWithMD5(password!);
                if (user.UserPassword != hashedPassword)
                    throw new UnauthorizedAccessException(AppStrings.Messages.InvalidPwd);

                // Token generation
                var token = _tokenService.GenerateToken(user);
                await _repository.UpdateUserTokenAsync(user.UserId!, token);

                // Log entry
                var ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString()
                    ?? AppStrings.Constants.UnKnown;

                await _userLogRepository.CreateAsync(new UserLog
                {
                    UserId = user.UserId,
                    IPAddress = ipAddress,
                    UserLogStatus = AppStrings.Status.I,
                    UserLoginTime = DateTime.UtcNow,
                    UserLogEntryDate = DateTime.UtcNow,
                    UserLogUpdateDate = DateTime.UtcNow,
                });

                await _userDataDetailsRepository.CreateAsync(new UserDataDetails
                {
                    UserId = user.UserId,
                    BearerToken = token,
                    OrganizationId = organization.Id,
                    UserEntryDate = DateTime.UtcNow
                });

                // Return response
                return new UserManagementResponse
                {
                    UserId = user.UserId,
                    UserMasterId = user.UserMasterId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserMobileNo = user.UserMobileNo,
                    OrganizationName = organization.OrganizationName,
                    UserBearerToken = token,
                    UserMasterRole = userMaster?.UserMasterRole
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{AppStrings.Constants.LoginHandler} {AppStrings.Constants.Exception}: {ex.Message}");
                throw;
            }
        }
    }
}
