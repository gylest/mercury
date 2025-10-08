using System;
using System.Collections.Generic;
using System.Text.Json;
using AngularClient.Models;
using AngularClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AngularClient.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        readonly CustomersService customersService;
        private readonly ILogger _logger;

        public CustomersController(IConfiguration configuration, ILogger<CustomersController> logger)
        {
            customersService = new CustomersService(configuration.GetConnectionString("OctobridgeDatabase"));
            _logger = logger;
        }

        // GET: api/Customers
        // GET: api/Customers?lastName=Gyles&firstName=Tony
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomer([FromQuery] string? lastName, [FromQuery] string? firstName)
        {
            List<Customer> customers;

            try
            {
                if ((lastName is null) && (firstName is null))
                {
                    customers = customersService.GetAllCustomers();

                    // Log 
                    _logger.LogInformation($"Customers (Get All): Count: {customers.Count}");
                }
                else
                {
                    customers = customersService.GetCustomersByName(lastName ?? string.Empty, firstName ?? string.Empty);

                    // Log 
                    _logger.LogInformation($"Customers (Get By Name): LastName: {lastName ?? string.Empty} FirstName: {firstName ?? string.Empty} Count: {customers.Count}");
                }

                return Ok(customers);
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in retrieving customers. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

        // POST: api/Customers
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }

                customersService.AddCustomer(customer);

                // Log 
                string customerJson = JsonSerializer.Serialize(customer);
                _logger.LogInformation($"Customers (Add): {customerJson}");

                return Ok(customer);
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in adding customer. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }

                customersService.UpdateCustomer(customer);
                _logger.LogInformation($"Customers (Update): FirstName: {customer.FirstName} LastName: {customer.LastName} Address Line 1: {customer.AddressLine1}");

                return Ok(customer);
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in updating order. Source: {exc.Source}. Message: {exc.Message}.");
            }

        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                customersService.DeleteCustomer(id);

                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in deleting customer. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

    }
}
