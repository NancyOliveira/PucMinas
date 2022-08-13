using Domain.DTO.Customer;

namespace Infra.Data.Customer
{
    public interface ICustomerReader
    {
        Task<bool> ExistsAsync(string document);

        Task<CustomerDTO> GetDocumentAsync(string document);
    }
}