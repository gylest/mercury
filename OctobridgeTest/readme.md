# OctobridgeTest

This solution contains two `NUnit` test projects targeting .NET.

---

## Projects Overview

### 1. TestDatabaseNUnit

**Purpose:**  
Automates database testing for the Octobridge SQL Server database using Entity Framework Core.  

**Technologies:**  
- NUnit Test Framework  
- Entity Framework Core  
- Using GitHUb NuGet package `OctobridgeEF` for database models  

**Setup & Usage:**  
- Project type: `NUnit Test Project (C#)`  
- NuGet packages:  
  - NUnit, NUnit3TestAdapter, Microsoft.Net.Test.Sdk  
  - Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools  
  - Microsoft.Extensions.Configuration.Json  
- Reverse engineer the database using the `Scaffold-DbContext` command to generate models.  
- Ensure the "Octobridge" database exists and the connection string is correct.  
- Run tests via `Test Explorer` in Visual Studio.  

---

### 2. TestRestServicesNUnit

**Purpose:**  
Automates REST API testing for the OctobridgeCoreRestService using HttpClient.  

**Technologies:**  
- NUnit Test Framework  
- HttpClient (System.Net.Http)  

**Setup & Usage:**  
- Project type: `NUnit Test Project (C#)`  
- NuGet packages:  
  - NUnit, NUnit3TestAdapter, Microsoft.Net.Test.Sdk  
  - Microsoft.AspNet.WebApi.Client, Microsoft.AspNetCode.StaticFiles  
- Ensure the REST service `OctobridgeCoreRestService` is running (IIS Express or IIS).  
- Run tests via `Test Explorer` in Visual Studio.  

---

## Getting Started

1. Open the solution in Visual Studio 2022.  
2. Restore NuGet packages.  
3. For database tests, verify the SQL Server and connection string.  
4. For REST API tests, `start` the REST service.  
5. Use `Test Explorer` to run and review test results.  

---

## Notes

- Both test projects are designed for automated testing and require minimal manual setup beyond service/database availability.  
- Target framework: `.NET 10`  