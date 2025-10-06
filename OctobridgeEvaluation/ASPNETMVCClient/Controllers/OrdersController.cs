using MVCClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Collections.Generic;

namespace MVCClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OctobridgeContext _context;
        private readonly ILogger           _logger;

        public OrdersController(OctobridgeContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            // ViewData is used to transfer data from controller => View
            // ViewData is a dictionary which can contain key-value pairs where each key must be string
            ViewData["IdParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["OrderStatusParam"] = sortOrder == "orderstatus" ? "orderstatus_desc" : "orderstatus";
            ViewData["TotalDueParam"] = sortOrder == "totaldue" ? "totaldue_desc" : "totaldue";

            var orders = from s in _context.Orders
                         select s;

            orders = sortOrder switch
            {
                "id_desc" => orders.OrderByDescending(s => s.Id),
                "orderstatus" => orders.OrderBy(s => s.OrderStatus),
                "orderstatus_desc" => orders.OrderByDescending(s => s.OrderStatus),
                "totaldue" => orders.OrderBy(s => s.TotalDue),
                "totaldue_desc" => orders.OrderByDescending(s => s.TotalDue),
                _ => orders.OrderBy(s => s.Id),
            };
            return View(await orders.ToListAsync().ConfigureAwait(true));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(true);

            if (order == null)
            {
                _logger.LogWarning($"OrdersController - Fail (Get) : Id = {id}");
                return NotFound();
            }

           _logger.LogInformation($"OrdersController - Success (Get) : Id = {id}, OrderStatus={order.OrderStatus}");

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderStatus,FirstName,LastName,MiddleName,ProductId,Amount,OrderDate,RecordModified")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync().ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderStatus,FirstName,LastName,MiddleName,ProductId,Amount,OrderDate,RecordModified")] Order order)
        {
            List<Order> orderList = new List<Order>();

            if ((order==null) || (id != order.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orderList = await _context.Orders.FromSqlInterpolated($"UpdateOrderById {order.Id},{order.OrderStatus},{DateTime.Now}").ToListAsync();

                    string orderJson = JsonSerializer.Serialize(orderList.FirstOrDefault());
                    _logger.LogInformation($"Order (Put): {orderJson}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(true);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync().ConfigureAwait(true);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
