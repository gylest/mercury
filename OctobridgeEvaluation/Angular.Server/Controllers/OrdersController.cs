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
    public class OrdersController : ControllerBase
    {
        readonly OrdersService ordersService;
        private readonly ILogger _logger;

        public OrdersController(IConfiguration configuration, ILogger<CustomersController> logger)
        {
            ordersService = new OrdersService(configuration.GetConnectionString("OctobridgeDatabase"));
            _logger = logger;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                Order[] orders = ordersService.GetAllOrders();

                return Ok(orders);
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in retrieving orders. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return BadRequest("Method not allowed");
        }

        // POST: api/Orders
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest();
                }

                ordersService.AddOrder(order);

                // log
                string orderJson = JsonSerializer.Serialize(order);
                _logger.LogInformation($"Orders (Add): {orderJson}");

                return Ok(order);
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in adding order. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order, [FromQuery] DateTime orderDate)
        {
            if (order == null)
            {
                return BadRequest();
            }

            try
            {
                order.Id = id;

                ordersService.UpdateOrder(order, orderDate);

                return Ok(order);
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in updating order. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ordersService.DeleteOrder(id);

                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in deleting order. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

    }
}
