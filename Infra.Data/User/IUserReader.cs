namespace Infra.Data.User
{
    public interface IUserReader
    {
        Task<bool> ExistsAsync(string login, string password);
    }
}