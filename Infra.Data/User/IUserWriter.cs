namespace Infra.Data.User
{
    public interface IUserWriter
    {
        Task UpdatePassword(string login, string passwordOld, string passwordNew);
    }
}