using Domain.DTO.Customer;

namespace Infra.Data.Customer
{
    public interface ICustomerWriter
    {
        Task AddAsync(CustomerDTO customerDTO);
    }
}