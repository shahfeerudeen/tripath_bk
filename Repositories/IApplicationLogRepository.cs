namespace tripath.Repositories
{
public interface IApplicationLogRepository
{
    Task LogAsync(string action, string userId);
    Task LogAsync(string action);
}
}