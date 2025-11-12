# MVCClient

## Overview
MVCClient is an ASP.NET Core MVC (Model-View-Controller) Web Application targeting .NET.  
It provides a web interface for managing entities such as Orders, Customers, Products, and more, backed by a SQL Server database using Entity Framework Core.  

---

## Project Structure

- **Controllers/**  
  MVC controllers for handling requests and returning views (e.g., `OrdersController`, `MoviesController`).
- **Models/**  
  Entity classes generated from the Octobridge database (e.g., `Order`, `Customer`, `Product`, etc.).
- **Repositories/**  
  Classes and interfaces for data access and business logic following the repository pattern.
- **Views/**  
  Razor views for displaying and editing data.
- Configuration File  
  The `appsettings.json` file stores connection strings and application settings.
- **Program.cs / Startup.cs**  
  Application startup and service registration.
- Developer Notes  
  File `ASPNETMVCDeveloperNotes.txt` has notes for ASP.NET MVC development.

## Web Pages

- **Home**  
  Description of the application features.
- **Help**  
  Help page with FAQs and documentation. (work in progress))
- **Privacy**  
  Privacy policy information. (work in progress)
- **Customers**  
  Manage products in the database. Add, edit and delete customers.
- **Movies**  
  View and add movies to an in-memory database
- **Orders**  
  View orders in the database.
- **Products**  
  Manage products in the database. Add, edit and delete products.

---

## MVC Controllers & REST API

The MVC controllers in this application are intended for server side rendering of Razor Views,
which are then sent to the client browser.

There are no dedicated REST API endpoints in this project.
But they could be added as an enhancement in future.

---
## Setup & Build Instructions

1. Restore NuGet packages
2. Select `Build MVCClient` from Build menu

---