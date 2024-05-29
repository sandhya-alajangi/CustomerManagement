using CustomerManagement.Application;
using CustomerManagement.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAPIClient _aPIClient;

        public CustomerController(IAPIClient aPIClient)
        {
            _aPIClient = aPIClient;
        }

        /// <summary>
        /// Displays a list of all customers.
        /// </summary>
        /// <returns>A view with a list of customers.</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Customer> customersList = await _aPIClient.GetList<Customer>();
                return View(customersList);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error loading customer list: " + ex.Message;
                return View(new List<Customer>());
            }
        }

        /// <summary>
        /// Displays the create customer form.
        /// </summary>
        /// <returns>A view with the create customer form.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer object to create.</param>
        /// <returns>A redirect to the index action if successful, otherwise returns the same view.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _aPIClient.Create(customer);
                    TempData["success"] = "Customer created successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error creating customer: " + ex.Message;
                }
            }
            return View();
        }

        /// <summary>
        /// Displays the edit customer form for a specific customer.
        /// </summary>
        /// <param name="id">The ID of the customer to edit.</param>
        /// <returns>A view with the edit customer form.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                Customer customer = await _aPIClient.GetById<Customer>(id);
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error loading customer: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customerRequest">The updated customer information.</param>
        /// <param name="id">The ID of the customer to update.</param>
        /// <returns>A redirect to the index action if successful, otherwise returns the same view.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Customer customerRequest, Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _aPIClient.Update<Customer>(customerRequest, id);
                    TempData["success"] = "Customer updated successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error updating customer: " + ex.Message;
                }
            }
            return View();
        }

        /// <summary>
        /// Displays the delete customer confirmation form for a specific customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>A view with the delete customer confirmation form.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = await _aPIClient.GetById<Customer>(id);
            return View(customer);
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>A redirect to the index action if successful, otherwise returns the same view.</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            try
            {
                Customer customer = await _aPIClient.GetById<Customer>(id);
                if (customer == null)
                {
                    return NotFound();
                }
                await _aPIClient.Delete(id);
                TempData["success"] = "Customer deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error deleting customer: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
