using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OctobridgeCoreRestService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using OctobridgeEF.Models;

namespace OctobridgeCoreRestService.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
//   [Route("api/v1/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly OctobridgeContext _context;
        private readonly ILogger _logger;

        public CustomersController(OctobridgeContext context, ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //
        // GET  :  https://localhost:44366/api/v1/customers/2
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            // Log 
            string customerJson = JsonSerializer.Serialize(customer);
            _logger.LogInformation($"Get Customer by [id] = [{id}]: {customerJson}");

            return customer;
        }

        //
        // GET: https://localhost:44366/api/v1/customers
        // GET: https://localhost:44366/api/v1/customers?lastName=gyles&firstName=ton
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer([FromQuery] string lastName, [FromQuery] string firstName)
        {
            List<Customer> customers;

            if ((lastName is null) && (firstName is null))
            {
                customers = await _context.Customers.ToListAsync().ConfigureAwait(true);
            }
            else
            {
                customers = await _context.Customers.FromSqlInterpolated($"GetCustomersByName {lastName},{firstName}").ToListAsync().ConfigureAwait(true);
            }

            if (customers.Count == 0)
            {
                return NotFound();
            }

            // Log 
            string customersJson = JsonSerializer.Serialize(customers);
            _logger.LogInformation($"Get Customers by [lastname,first] = [{lastName},{firstName}]: {customersJson}");

            return customers;
        }

        //
        // PUT: https://localhost:44366/api/v1/customers/2
        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if ((customer == null) || (id != customer.Id))
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //
        // POST: https://localhost:44366/api/v1/customers
        //
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync().ConfigureAwait(true);

                // Log 
                string customerJson = JsonSerializer.Serialize(customer);
                _logger.LogInformation($"Customer (Post): {customerJson}");

                // return
                return CreatedAtAction("GetCustomer", new { id = customer.Id, version = "1" }, customer);
            }
            catch (DbUpdateException exc)
            {
                return BadRequest($"Error in creating customer. Source: {exc.Source}. Message: {exc.Message}.");
            }
            catch
            {
                return BadRequest($"An unexpected error has occured.");
            }
        }

        //
        // DELETE: https://localhost:44366/api/v1/customers/2
        //
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }

}
