# DataMaintenanceEnhanced

A Visual C# .NET 9 Windows application for managing data via a RESTful service.

## Technology Highlights

- **Windows Presentation Foundation (WPF)**
- .NET 9

## Features

- Connects to a REST service (`OctobridgeCoreRestService`) for data operations
- Modern WPF user interface
- Configurable API endpoints via `App.config`

## Getting Started

### Prerequisites

- Visual Studio 2022 or later
- .NET 9 SDK
- The REST service `OctobridgeCoreRestService` running in IIS Express or IIS

### Configuration

Edit `App.config` to set the correct API endpoints:

```json
<add key="rootURI" value="https://localhost:44366/" />
<add key="productsURI" value="api/v1/product" />
```

### Running the Application

1. Start the `OctobridgeCoreRestService` in IIS Express (within Visual Studio) or IIS.
2. Build and run the `DataMaintenanceEnhanced` WPF application.
3. Perform data operations through the application's UI, which communicates with the REST service.

## Testing

- Ensure the REST service is running before launching the application.
- Use the application's UI to verify data operations (CRUD) are functioning as expected.

## Repository

This project is maintained in a Git repository: `https://git-codecommit.eu-west-1.amazonaws.com/v1/repos/Mercury`

