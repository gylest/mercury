using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OctobridgeCoreRestService.Services;
using OctobridgeCoreRestService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Asp.Versioning;

namespace OctobridgeCoreRestService.Controllers.V1
{    /// <summary>
     /// 
     /// IMPORTANT NOTE
     /// This controller has basic authentication. This requires a username/password to be added to HTTP request header.
     /// Therefore can only test via PostMan with basic authentication setup.
     /// 
     /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OctobridgeContext _context;
        private readonly IUserService      _userService;
        private readonly ILogger           _logger;

        public OrdersController(OctobridgeContext context, IUserService userService, ILogger<CustomersController> logger)
        {
            _context     = context;
            _userService = userService;
            _logger      = logger;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User userParam)
        {
            if (userParam == null)
                return BadRequest(new { message = "Invalid user" });

            var user = await _userService.Authenticate(userParam.Username, userParam.Password).ConfigureAwait(true);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        //
        // GET: https://localhost:44366/api/v1/orders/3
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        //
        // GET: https://localhost:44366/api/v1/orders
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Orders.ToListAsync().ConfigureAwait(true);
        }

        //
        // PUT: https://localhost:44366/api/v1/orders/7
        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if ((order == null) || (id != order.Id))
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        // POST: https://localhost:44366/api/v1/orders
        //
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest();
                }

                _context.Orders.Add(order);
                await _context.SaveChangesAsync().ConfigureAwait(true);

                // log
                string orderJson = JsonSerializer.Serialize(order);
                _logger.LogInformation($"Order (Post): {orderJson}");

                // return
                return CreatedAtAction("GetOrder", new { id = order.Id }, order);
            }
            catch
            {
                return BadRequest();
            }
        }

        //
        // DELETE: https://localhost:44366/api/v1/orders/4
        //
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id).ConfigureAwait(true);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
