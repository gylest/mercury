using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NUnit.Framework;
using Tests.Models;

//
// HTTP Status Code Ranges
// ========================
// 100 - 199 = Infomational
// 200 - 299 = Success
// 300 - 399 = Redirection
// 400 - 499 = Client Error
// 500 - 599 = Server Error

// Typical Codes
// ==============
// 200       = Success - Content Returned 
// 201       = Success - New content created
// 204       = Success - No Content Returned
// 400       = Bad Request. The server could not understand request
// 404       = Not Found. Basically URL not recognised.
// 500       = Internal server error. The server has encountered error it doesnt know how to handle
// 501       = Not Implemented. The request method is not supported by server

// REST Responses
// ==============
// GET ID    200 Success - Content Returned
//           404 Not Found
// GET ALL   200 Success - Content Returned
//           404 Not Found
// PUT ID    204 Success - No Content Returned
//           400 Bad Request (Correct Id not in payload)
//           404 Not Found
// POST ID   201 Success      - New Content Created
//           400 Bad Request (Payload has invalid or missing data)
// DELETE ID 200 Success
//           404 Not Found
namespace Tests
{
    public class CustomerTests
    {
        HttpClient     _client;
        const string   _base     = "https://localhost:44366/";
        List<Customer> _customers;

        [SetUp]
        public void Setup()
        {
            // Create 
            _client = new HttpClient();

            // Set data format and base address
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.BaseAddress = new Uri(_base);
        }

        [Test, Description("Get - Customer by Id - OK (200)")]
        public async Task Get_Customer_ById_Valid()
        {
            Uri      uri = new Uri("api/v1/customers/1", UriKind.Relative);
            Customer customer;

            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                // Get customer  
                customer = await response.Content.ReadAsAsync<Customer>().ConfigureAwait(true);

                Assert.Pass($"Customer returned: Id = {customer.Id}, Name = {customer.FirstName} {customer.LastName}");
            }
            else
            {
                Assert.Fail($"Invalid response code = {response.StatusCode}");
            }

        }

        [Test, Description("Get - Customer by Id on V2 - OK (200)")]
        public async Task Get_Customer_ById_Valid_v2()
        {
            Uri      uri = new Uri("api/v2/customers/1",UriKind.Relative);
            Customer customer;

            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                // Get customer  
                customer = await response.Content.ReadAsAsync<Customer>().ConfigureAwait(true);

                Assert.Pass($"Customer returned: Id = {customer.Id}, Name = {customer.FirstName} {customer.LastName}");
            }
            else
            {
                Assert.Fail($"Invalid response code = {response.StatusCode}");
            }

        }

        [Test, Description("Get - Customer by Id - Not Found (404)")]
        public async Task Get_Customer_ById_Invalid()
        {
            Uri      uri = new Uri("api/v1/customers/99999", UriKind.Relative);
            Customer customer;

            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                // Get customer  
                customer = await response.Content.ReadAsAsync<Customer>().ConfigureAwait(true);

                Assert.Fail($"Customer returned: Id = {customer.Id}, Name = {customer.FirstName} {customer.LastName}");
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

        [Test, Description("Get - Customers All - OK (200)")]
        public async Task Get_Customers_All()
        {
            Uri uri = new Uri("api/v1/customers", UriKind.Relative);

            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                // Get all customers  
                _customers = await response.Content.ReadAsAsync<List<Customer>>().ConfigureAwait(true);

                Assert.Pass($"Count of customers returned = {_customers.Count}");
            }
            else
            {
                Assert.Fail($"Invalid response code = {response.StatusCode}");
            }

        }

        [Test, Description("Get - Customer by name - OK (200)")]
        public async Task Get_Customer_ByName_Valid()
        {
            Uri uri = new Uri("api/v1/customers?lastName=gyles&firstName=tony", UriKind.Relative);

            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                // Get customers
                _customers = await response.Content.ReadAsAsync<List<Customer>>().ConfigureAwait(false);

                Assert.Pass($"Count of customers returned = {_customers.Count}");
            }
            else
            {
                Assert.Fail($"Invalid response code = {response.StatusCode}");
            }

        }

        [Test, Description("Get - Customer by name - Not Found (404)")]
        public async Task Get_Customer_ByName_Invalid()
        {
            Uri uri = new Uri("api/v1/customers?lastName=gerrald&firstName=bottom", UriKind.Relative);

            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                // Get customer  
                _customers = await response.Content.ReadAsAsync<List<Customer>>().ConfigureAwait(true);

                Assert.Fail($"Customers returned: Count = {_customers.Count}");
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

        [Test, Description("Delete - Customer by Id - OK (200)")]
        public async Task Delete_Customer_ById_Valid()
        {
            // Get last customer id
            Uri uriAll     = new Uri("api/v1/customers", UriKind.Relative);
            int lastId  = (-1);

            HttpResponseMessage response = await _client.GetAsync(uriAll).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                // Get all customers  
                _customers = await response.Content.ReadAsAsync<List<Customer>>().ConfigureAwait(true);

                // Get last id
                lastId = _customers[_customers.Count - 1].Id;
            }

            // Delete last customer
            Uri uri = new Uri($"api/v1/customers/{lastId}", UriKind.Relative);

            HttpResponseMessage responseDelete = await _client.DeleteAsync(uri).ConfigureAwait(true);

            if (responseDelete.IsSuccessStatusCode)
            {
                Assert.Pass($"Customer deleted successfully");
            }
            else
            {
                Assert.Fail($"Invalid response code = {response.StatusCode}");
            }

        }

        [Test, Description("Delete - Customer by Id - Not Found (404)")]
        public async Task Delete_Customer_ById_Invalid()
        {
            Uri uri = new Uri("api/v1/customers/99999", UriKind.Relative);

            HttpResponseMessage response = await _client.DeleteAsync(uri).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                Assert.Fail($"Expected failure as no customer exists!");
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

        [Test, Description("Post - Customer - OK (201)")]
        public async Task Post_Customer_Valid()
        {
            Uri      uri      = new Uri("api/v1/customers", UriKind.Relative);
            Customer customer = new Customer() { FirstName="Lucy", LastName="Thorn", AddressLine1="29 Casper Way", AddressLine2="Green Valley", City="Luton", PostalCode="LU1 1BB", Telephone="07776237819", Email="lucy111@btinternet.com"};

            HttpResponseMessage response = await _client.PostAsJsonAsync(uri, customer).ConfigureAwait(true);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Assert.Pass($"New customer created as expected.");
            }
            else
            {
                Assert.Fail($"Invalid response code = {response.StatusCode}");
            }

        }

        [Test, Description("Post - Customer - OK (201)")]
        public async Task Post_Customer_Valid_v2()
        {
            Uri uri = new Uri("api/v2/customers", UriKind.Relative);
            Customer customer = new Customer() { FirstName = "Trevor", LastName = "Smith", AddressLine1 = "77 Icarus Avenue", AddressLine2 = "Whitehaven", City = "Blackpool", PostalCode = "BP29 3LZ", Telephone = "0653535637", Email = "trev123@gmail.com" };

            HttpResponseMessage response = await _client.PostAsJsonAsync(uri, customer).ConfigureAwait(true);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Assert.Pass($"New customer created as expected.");
            }
            else
            {
                Assert.Fail($"Invalid response code = {response.StatusCode}");
            }

        }

        [Test, Description("Post - Customer - Bad Request (400)")]
        public async Task Post_Customer_Invalid()
        {
            Uri      uri      = new Uri("api/v1/customers", UriKind.Relative);
            Customer customer = new Customer() { FirstName = "Tim", LastName = "Bottom", City = "Bosworth", Email = "timbottom@google.com" };

            HttpResponseMessage response = await _client.PostAsJsonAsync(uri, customer).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                Assert.Fail($"EXpected customer creation to fail as key information missing!");
            }
            else
            {
                Assert.Pass($"Status code = {response.StatusCode}");
            }

        }

        [Test, Description("Get - Customer by Id - OK (204)")]
        public async Task Put_Customer_Valid()
        {
            DateTime modifiedTime = DateTime.Now; 
            Customer customer     = new Customer() { Id = 0, FirstName = "Stephen", LastName = "Gyles", AddressLine1 = "12 Ringshall Gardens",  City = "Bramley", PostalCode = "RG26 5BW", Telephone = "077711223344", Email = "steve.gyles@ntlworld.com", RecordModified = modifiedTime };
            Customer lastCustomer;

            // Get last customer 
            Uri uriAll = new Uri("api/v1/customers", UriKind.Relative);

            HttpResponseMessage responseGet = await _client.GetAsync(uriAll).ConfigureAwait(true);

            if (responseGet.IsSuccessStatusCode)
            {
                // Get all customers  
                _customers = await responseGet.Content.ReadAsAsync<List<Customer>>().ConfigureAwait(true);

                // Get last customer
                lastCustomer = _customers[_customers.Count - 1];

                // Update last customer
                customer.Id = lastCustomer.Id;
                Uri uri = new Uri($"api/v1/customers/{customer.Id}", UriKind.Relative);

                HttpResponseMessage responsePut = await _client.PutAsJsonAsync(uri, customer).ConfigureAwait(true);

                if (responsePut.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Assert.Pass($"Customer update succeeded.");
                }
                else
                {
                    Assert.Fail($"Customer update failed = {responsePut.StatusCode}");
                }
            }
            else
            {
                Assert.Fail($"Status code = {responseGet.StatusCode}");
            }

        }

        [Test, Description("Put - Customer by Id - Bad Request (400)")]
        public async Task Put_Customer_Invalid_Data()
        {
            // For this test case the uri (3) and customer(7) do not match!
            Uri uri = new Uri("api/v1/customers/3", UriKind.Relative);
            Customer customer = new Customer() { Id = 7, FirstName = "Stephen", LastName = "Gyles", AddressLine1 = "12 Ringshall Gardens", City = "Bramley", PostalCode = "RG26 5BW", Telephone = "077711223344", Email = "steve.gyles@ntlworld.com" };

            HttpResponseMessage response = await _client.PutAsJsonAsync(uri, customer).ConfigureAwait(true);

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

        [Test, Description("Put - Customer by Id - Not Found (404)")]
        public async Task Put_Customer_Invalid()
        {
            Uri      uri      = new Uri("api/v1/customers/99999", UriKind.Relative);
            Customer customer = new Customer() { Id = 99999, FirstName = "Lucy", LastName = "Thorn", AddressLine1 = "29 Casper Way", AddressLine2 = "Green Valley", City = "Luton", PostalCode = "LU1 1BB", Telephone = "07776237819", Email = "lucy111@btinternet.com" };

            HttpResponseMessage response = await _client.PutAsJsonAsync(uri, customer).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                Assert.Fail($"Expected customer update to fail as customer does not exist!");
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

}
