using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OctobridgeCoreRestService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OctobridgeCoreRestService.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    //   [Route("api/v2/customers")]
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
        // GET  :  https://localhost:44366/api/v2/customers/5
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            // Convert customer to uppercase
            var customerType = typeof(Customer);
            foreach (var prop in customerType.GetProperties())
            {
                if (prop.PropertyType == typeof(string) && prop.CanRead && prop.CanWrite)
                {
                    var value = (string)prop.GetValue(customer);
                    if (value != null)
                    {
                        prop.SetValue(customer, value.ToUpperInvariant());
                    }
                }
            }

            return customer;
        }

        //
        // GET: https://localhost:44366/api/v2/customers
        // GET: https://localhost:44366/api/v2/customers?lastName=gyles&firstName=ton
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

            // Convert customers to upper case
            var customerType = typeof(Customer);
            foreach (var customer in customers)
            {
                foreach (var prop in customerType.GetProperties())
                {
                    if (prop.PropertyType == typeof(string) && prop.CanRead && prop.CanWrite)
                    {
                        var value = (string)prop.GetValue(customer);
                        if (value != null)
                        {
                            prop.SetValue(customer, value.ToUpperInvariant());
                        }
                    }
                }
            }

            return customers;
        }

        //
        // PUT: https://localhost:44366/api/v2/customers/2
        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if ((customer==null) || (id != customer.Id))
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
        // POST: https://localhost:44366/api/v2/customers
        //
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            // Log 
            string customerJson = JsonSerializer.Serialize(customer);
            _logger.LogInformation($"Customer (Post): {customerJson}");

            // return
            return CreatedAtAction("GetCustomer", new { id = customer.Id, version = "2" }, customer);
        }

        //
        // DELETE: https://localhost:44366/api/v2/customers/2
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
