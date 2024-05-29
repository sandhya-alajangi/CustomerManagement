using CustomerManagement.Domain;

namespace CustomerManagement.Application
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(Guid id);
        Task InsertCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customerRequest, Guid id);
        Task<Customer> DeleteCustomer(Guid id);
    }
}
