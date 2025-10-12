namespace MVCClient.Controllers;

public class CustomersController : Controller
{
    private readonly OctobridgeContext _context;
    private readonly ILogger _logger;

    public CustomersController(OctobridgeContext context, ILogger<CustomersController> logger)
    {
        _context = context;
        _logger  = logger;
    }

    // GET: Customers
    public async Task<IActionResult> Index()
    {
        return View(await _context.Customers.ToListAsync().ConfigureAwait(true));
    }

    // GET: Customers/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(true);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Customers/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,MiddleName,AddressLine1,AddressLine2,City,PostalCode,Telephone,Email,RecordCreated,RecordModified")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync().ConfigureAwait(true);
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

    // GET: Customers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    // POST: Customers/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,MiddleName,AddressLine1,AddressLine2,City,PostalCode,Telephone,Email,RecordCreated,RecordModified")] Customer customer)
    {
        List<Customer> customerList = new List<Customer>();

        if ((customer==null) || (id != customer.Id))
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                customerList = await _context.Customers.FromSqlInterpolated($"UpdateCustomerById {customer.Id},{customer.FirstName},{customer.LastName},{customer.MiddleName},{customer.AddressLine1},{customer.AddressLine2}, {customer.City},{ customer.PostalCode},{ customer.Telephone},{customer.Email}").ToListAsync();

                string customerJson = JsonSerializer.Serialize(customerList.FirstOrDefault());
                _logger.LogInformation($"Customer (Put): {customerJson}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Id))
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
        return View(customer);
    }


    // GET: Customers/Delete/5
    // IMportant note: This does look internalkly same as edit!
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(true);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: Customers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync().ConfigureAwait(true);
        return RedirectToAction(nameof(Index));
    }

    private bool CustomerExists(int id)
    {
        return _context.Customers.Any(e => e.Id == id);
    }
}
