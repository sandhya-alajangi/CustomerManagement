using CustomerManagement.Application;
using CustomerManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure
{
    /// <summary>
    /// Repository class for managing customer data.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerRepository(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a list of all customers.
        /// </summary>
        /// <returns>A list of customers.</returns>
        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> names = await _dbContext.Customers.ToListAsync();
            return names;
        }

        /// <summary>
        /// Retrieves a single customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer with the specified ID, or null if the customer does not exist.</returns>
        public async Task<Customer> GetCustomer(Guid id)
        {
            Customer customer = await _dbContext.Customers.FindAsync(id);
            return customer;
        }


        /// <summary>
        /// Inserts a new customer into the database.
        /// </summary>
        /// <param name="customer">The customer to insert.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InsertCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>The deleted customer, or null if the customer does not exist.</returns>
        public async Task<Customer> DeleteCustomer(Guid id)
        {
            Customer customerRequest = await _dbContext.Customers.FindAsync(id);
            if (customerRequest != null)
            {
                _dbContext.Customers.Remove(customerRequest);
                await _dbContext.SaveChangesAsync();
            }
            return customerRequest;
        }

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="customer">The updated customer information.</param>
        /// <param name="id">The ID of the customer to update.</param>
        /// <returns>The updated customer, or null if the customer does not exist.</returns>
        public async Task<Customer> UpdateCustomer(Customer customer, Guid id)
        {
            Customer customerRequest = await _dbContext.Customers.FindAsync(id);
            if (customerRequest == null)
            {
                return null;
            }
            customerRequest.FirstName = customer.FirstName;
            customerRequest.LastName = customer.LastName;
            customerRequest.Email = customer.Email;
            customerRequest.PhoneNumber = customer.PhoneNumber;
            await _dbContext.SaveChangesAsync();
            return customerRequest;
        }
    }
}
