# Angular.Server

This project is an ASP.NET Core Web API backend targeting .NET.  
It provides RESTful endpoints for managing customers, orders, products, order details, and attachments, and is designed to work with the Angular.Client frontend.

## Features

- REST API for customers, orders, products, order details, and attachments
- Integration with SQL Server via service classes
- Logging for key operations
- Designed for use with Angular and CORS support

## Project Structure

- `Controllers/` - API controllers for each entity (Customers, Orders, Products, OrderDetails, Attachments)
- `Models/` - Entity classes representing database tables
- `Services/` - Business logic and data access for each entity
- `Program.cs` - Application startup and middleware configuration

## Controllers & REST API Endpoints

### CustomersController

Base route: `/api/Customers`

| Method | Route                                   | Description                                      |
|--------|-----------------------------------------|--------------------------------------------------|
| GET    | `/api/Customers`                        | Get all customers                                |
| GET    | `/api/Customers?lastName=&firstName=`   | Get customers filtered by last and first name    |
| POST   | `/api/Customers`                        | Add a new customer (JSON body)                   |
| PUT    | `/api/Customers/{id}`                   | Update a customer by ID (JSON body)              |
| DELETE | `/api/Customers/{id}`                   | Delete a customer by ID                          |

---

### OrdersController

Base route: `/api/Orders`

| Method | Route                | Description                        |
|--------|----------------------|------------------------------------|
| GET    | `/api/Orders`        | Get all orders                     |
| GET    | `/api/Orders/{id}`   | Not allowed (returns BadRequest)   |
| POST   | `/api/Orders`        | Add a new order (JSON body)        |
| PUT    | `/api/Orders/{id}`   | Update an order by ID (JSON body)  |
| DELETE | `/api/Orders/{id}`   | Delete an order by ID              |

---

### ProductsController

Base route: `/api/Products`

| Method | Route                                         | Description                                      |
|--------|-----------------------------------------------|--------------------------------------------------|
| GET    | `/api/Products`                               | Get all products                                 |
| GET    | `/api/Products?name=&productNumber=`          | Get products filtered by name and product number |
| GET    | `/api/Products/{productId}`                   | Get a product by ID                              |
| POST   | `/api/Products`                               | Add a new product (JSON body)                    |
| PUT    | `/api/Products/{id}`                          | Update a product by ID (JSON body)               |

---

### OrderDetailsController

Base route: `/api/OrderDetails`

| Method | Route                      | Description                        |
|--------|----------------------------|------------------------------------|
| GET    | `/api/OrderDetails/{orderId}` | Get order details for an order ID  |

---

### AttachmentsController

Base route: `/api/Attachments`

| Method | Route                        | Description                                 |
|--------|------------------------------|---------------------------------------------|
| GET    | `/api/Attachments`           | Get all attachments                         |
| GET    | `/api/Attachments/{id}`      | Get a specific attachment by ID             |
| POST   | `/api/Attachments`           | Add a new attachment (JSON body or file)    |
| PUT    | `/api/Attachments/{id}`      | Update an attachment by ID (JSON body)      |
| DELETE | `/api/Attachments/{id}`      | Delete an attachment by ID                  |

---

## Serilog Logging

The application uses Serilog for logging. The logging configuration is in the appsettings.json file.  

Log settings:  
1. Sinks enabled for logging to console and File  
2. Default Log Level =  Information  
3. Log File stored in logs/log-.txt (daily rolling)  
4. A new log file is created each day  
5. Retain log files for 28 days  

---

## Getting Started

1. Restore NuGet packages.
2. Update your connection string in `appsettings.json` if needed.
3. Build and run the project using Visual Studio or `dotnet run`.
4. The API will be available at <https://localhost:63498/> (or your configured port).

## Notes

- Ensure CORS is enabled if accessing from a frontend on a different port.
- Logging is implemented for key operations.
- The API expects and returns JSON.
- For full API documentation, consider enabling Swagger/OpenAPI in development.

## License

This project is licensed under the MIT License.