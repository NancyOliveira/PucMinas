namespace Domain.DTO.User
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}