# OctobridgeCoreRestService

A REST API project for managing Octobridge database entities, built with ASP.NET Core (.NET 9).

---

## Features

- Entity Framework Core integration with SQL Server
- API versioning (v1, v2, etc.)
- Interactive Swagger UI for API exploration
- Health checks endpoint
- Basic authentication support
- CORS configuration for local development

---

## REST API

Controller        | Operations               | GET API Example
-------------     |--------------------------|----------------------------
Attachments       | GET, POST, DELETE        | api/v1/attachments?filename=helfer.jpg
Customers         | GET, POST, PUT, DELETE   | api/v1/customers?lastName=Gyles&firstName=Tony
Orders            | GET, POST, PUT, DELETE   | api/v1/orders/5
Products          | GET, POST, PUT, DELETE   | api/v1/products/4

Customers supports ```api/v2``` and the GET operations return upper case data.  

---

## Swagger UI

   - Swagger is enabled for interactive API documentation and testing.
   - Access Swagger UI at: <https://localhost:44366/swagger>
   - Use Swagger to explore and test all available endpoints, including each API version (e.g., v1, v2). You can view request/response schemas, try out endpoints, and inspect results directly in the browser.
   - Swagger also provides generated JSON for each API version, e.g.:
     - <https://localhost:44366/swagger/v1/swagger.json>
     - <https://localhost:44366/swagger/v2/swagger.json>

---

## Testing

1. REST Client Files
   - The project includes `.rest` files for manual API testing. These files contain HTTP request definitions that can be executed directly from Visual Studio or compatible REST clients.
   - To use: Open a `.rest` file, select a request, and run it to send HTTP requests to the API endpoints. This allows you to quickly verify responses, status codes, and payloads for each controller and method.
   - `.rest` files are especially useful for regression testing and verifying changes during development.

2. Postman
   - The Orders endpoints require Basic Authentication and must be tested using Postman.
   - In Postman, set the request type and URL for the Orders API (e.g., `https://localhost:44366/api/v1/orderscore`).
   - Under the "Authorization" tab, select "Basic Auth" and enter the required username and password.
   - Send requests (GET, POST, PUT, DELETE) to verify authentication and endpoint functionality.
   - Use Postman to confirm correct responses, error handling, and security for all Orders API methods.

---

## NuGet Packages

- `Microsoft.EntityFrameworkCore.SqlServer` - Entity Framework Core provider for SQL Server
- `Swashbuckle.AspNetCore` - Swagger/OpenAPI documentation generation
- `Asp.Versioning.Mvc` - API versioning for ASP.NET Core
- `Asp.Versioning.ApiExplorer` - API versioning support for API Explorer/Swagger
- `Microsoft.AspNetCore.Authentication` - Authentication support
- `Microsoft.Extensions.HealthChecks` - Health checks for ASP.NET Core

---

## Getting Started

### Prerequisites

- .NET 9 SDK  
- SQL Server 2022 or later  
- Visual Studio 2022 or later  

---

## Improvements

The following improvements could enhance the quality, maintainability, and performance of the OctobridgeCoreRestService project:

- **Comprehensive Unit and Integration Testing**  
  Add automated tests for controllers, services, and data access to ensure reliability and catch regressions early.

- **Error Handling and Validation**  
  Implement consistent error handling and input validation across all endpoints, using middleware and data annotations where appropriate.

- **Async/Await Best Practices**  
  Review and optimize asynchronous code to avoid unnecessary context captures and improve scalability.

- **Logging Enhancements**  
  Expand structured logging for better traceability and easier debugging, including correlation IDs for request tracking.

- **API Documentation**  
  Enrich Swagger/OpenAPI documentation with detailed descriptions, example requests/responses, and authentication instructions.

- **Security Improvements**  
  Strengthen authentication and authorization, consider using OAuth2/JWT, and ensure sensitive data is never logged.

- **Pagination and Filtering**  
  Add support for pagination, filtering, and sorting on list endpoints to improve performance and usability for large datasets.

- **Configuration Management**  
  Externalize configuration (e.g., connection strings, secrets) using environment variables or Azure Key Vault for better security and flexibility.

- **Performance Optimization**  
  Profile and optimize database queries, use asynchronous streaming for large result sets, and consider caching frequently accessed data.
