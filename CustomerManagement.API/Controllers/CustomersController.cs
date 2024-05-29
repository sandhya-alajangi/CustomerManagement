using CustomerManagement.Application;
using CustomerManagement.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Retrieves a list of all customers.
        /// </summary>
        /// <returns>A list of customers.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            List<Customer> list = await _repository.GetCustomers();
            return list;
        }


        /// <summary>
        /// Retrieves a single customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer with the specified ID, or a 404 Not Found if the customer does not exist.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
        {
            Customer customer = await _repository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer to create.</param>
        /// <returns>The created customer.</returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            await _repository.InsertCustomer(customer);
            return CreatedAtAction("GetCustomerById", new { id = customer.Id }, customer);
        }


        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customerRequest">The updated customer information.</param>
        /// <param name="id">The ID of the customer to update.</param>
        /// <returns>The updated customer, or a 404 Not Found if the customer does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer customerRequest, Guid id)
        {
            Customer customer = await _repository.UpdateCustomer(customerRequest, id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        /// <summary>
        /// Deletes a customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>A 200 OK with a success message if the customer was deleted, or a 404 Not Found if the customer does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(Guid id)
        {
            var customer = await _repository.DeleteCustomer(id);
            if ((customer == null))
            {
                return NotFound();
            }
            return Ok("Deleted Successfully");
        }

    }
}
