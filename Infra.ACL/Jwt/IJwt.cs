namespace Infra.ACL.Jwt
{
    public interface IJwt
    {
        string CreateToken(string login, DateTime expirationDate);
    }
}