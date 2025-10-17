namespace Tests;

public class OrderTests
{
    HttpClient     _client;
    const string   _base     = "https://localhost:44366/";
    List<Order> _orders;

    [SetUp]
    public void Setup()
    {
        const string userName = "tonygyles";
        const string userPwd  = "chelsea_1963";

        // Create 
        _client = new HttpClient();

        // Set data format and base address
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.BaseAddress = new Uri(_base);

        // Add basic authentication
        var authToken = Encoding.ASCII.GetBytes($"{userName}:{userPwd}");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
    }

    [Test, Description("Get - Order by Id - OK (200)")]
    public async Task Get_Order_ById_Valid()
    {
        Uri      uri = new Uri("api/v1/orders/1", UriKind.Relative);
        Order    order;

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            // Get order
            order = await response.Content.ReadAsAsync<Order>().ConfigureAwait(true);

            Assert.Pass($"Order returned: Id = {order.Id}, Status = {order.OrderStatus}, CustomerId = {order.CustomerId}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Get - Order by Id - Not Found (404)")]
    public async Task Get_Order_ById_Invalid()
    {
        Uri      uri = new Uri("api/v1/orders/99999", UriKind.Relative);

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"Order should not have been found!");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Assert.Pass($"Expected response code = {response.StatusCode}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Get - Orders All - OK (200)")]
    public async Task Get_Orders_All()
    {
        Uri uri = new Uri("api/v1/orders", UriKind.Relative);

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            // Get all orders  
            _orders = await response.Content.ReadAsAsync<List<Order>>().ConfigureAwait(true);

            Assert.Pass($"Count of orders returned = {_orders.Count}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Delete - Order by Id - OK (200)")]
    public async Task Delete_Order_ById_Valid()
    {
        // Get last order id
        Uri uriAll     = new Uri("api/v1/orders", UriKind.Relative);
        int lastId  = (-1);

        HttpResponseMessage response = await _client.GetAsync(uriAll).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            // Get all orders
            _orders = await response.Content.ReadAsAsync<List<Order>>().ConfigureAwait(true);

            // Get last id using index operator, ^1 means last element
            lastId = _orders[^1].Id;
        }

        // Delete last order
        Uri uri = new Uri($"api/v1/orders/{lastId}", UriKind.Relative);

        HttpResponseMessage responseDelete = await _client.DeleteAsync(uri).ConfigureAwait(true);

        if (responseDelete.IsSuccessStatusCode)
        {
            Assert.Pass($"Order deleted successfully");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Delete - Order by Id - Not Found (404)")]
    public async Task Delete_Order_ById_Invalid()
    {
        Uri uri = new Uri("api/v1/orders/99999", UriKind.Relative);

        HttpResponseMessage response = await _client.DeleteAsync(uri).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"Expected failure as no order exists!");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Assert.Pass($"Expected response code = {response.StatusCode}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Post - Order - OK (201)")]
    public async Task Post_Order_Valid()
    {
        Uri      uri      = new Uri("api/v1/orders", UriKind.Relative);

        const decimal freightCost = 10.00M;
        const decimal subTotal = 55.25M;

        var order = new Order()
        {
            OrderStatus = "Open",
            CustomerId = 3,
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync(uri, order).ConfigureAwait(true);

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            Assert.Pass($"New order created as expected.");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Post - Order - Bad Request (400)")]
    public async Task Post_Order_Invalid()
    {
        Uri      uri      = new Uri("api/v1/orders", UriKind.Relative);

        const decimal freightCost = 10.00M;
        const decimal subTotal = 55.25M;

        var order = new Order()
        {
            OrderStatus = "Open",
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync(uri, order).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"Expected order creation to fail as key information missing!");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            Assert.Pass($"Status code = {response.StatusCode}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Get - Order by Id - OK (204)")]
    public async Task Put_Order_Valid()
    {
        DateTime modifiedTime = DateTime.Now;

        const decimal freightCost = 10.00M;
        const decimal subTotal = 55.25M;

        var order = new Order()
        {
            OrderStatus = "Paid",
            CustomerId = 3,
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };
        
        Order    lastOrder;

        // Get last order 
        Uri uriAll = new Uri("api/v1/orders", UriKind.Relative);

        HttpResponseMessage responseGet = await _client.GetAsync(uriAll).ConfigureAwait(true);

        if (responseGet.IsSuccessStatusCode)
        {
            // Get all orders
            _orders = await responseGet.Content.ReadAsAsync<List<Order>>().ConfigureAwait(true);

            // Get last order
            lastOrder = _orders[^1];

            // Update last order
            order.Id = lastOrder.Id;
            Uri uri = new Uri($"api/v1/orders/{order.Id}", UriKind.Relative);

            HttpResponseMessage responsePut = await _client.PutAsJsonAsync(uri, order).ConfigureAwait(true);

            if (responsePut.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Assert.Pass($"Order update succeeded.");
            }
            else
            {
                Assert.Fail($"Order update failed = {responsePut.StatusCode}");
            }
        }
        else
        {
            Assert.Fail($"Invalid response code = {responseGet.StatusCode}");
        }

    }

    [Test, Description("Put - Order by Id - Bad Request (400)")]
    public async Task Put_Order_Invalid_Data()
    {
        const decimal freightCost = 10.00M;
        const decimal subTotal = 55.25M;

        // For this test case the uri (3) and order(7) do not match!
        Uri uri = new Uri("api/v1/orders/3", UriKind.Relative);

        var order = new Order()
        {
            Id = 7,
            OrderStatus = "Open",
            CustomerId = 3,
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };
        HttpResponseMessage response = await _client.PutAsJsonAsync(uri, order).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"This is a bad request and should fail.");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            Assert.Pass($"Status code = {response.StatusCode}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Put - Order by Id - Not Found (404)")]
    public async Task Put_Order_Invalid()
    {
        Uri      uri      = new Uri("api/v1/orders/99999", UriKind.Relative);
        const decimal freightCost = 10.00M;
        const decimal subTotal = 55.25M;

        var order = new Order()
        {
            Id = 99999,
            CustomerId =2,
            OrderStatus = "Open",
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };
        HttpResponseMessage response = await _client.PutAsJsonAsync(uri, order).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"Expected order update to fail as order does not exist!");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Assert.Pass($"Expected response code = {response.StatusCode}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

}
