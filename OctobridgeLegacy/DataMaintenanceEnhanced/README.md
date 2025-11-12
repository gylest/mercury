# DataMaintenanceEnhanced

A Visual C# .NET Windows application for managing data via a RESTful service.

## Technology Highlights

- **Windows Presentation Foundation (WPF)**
- Updating data through RESTful API calls

## Features

- Connects to a REST service (`OctobridgeCoreRestService`) for data operations
- Modern WPF user interface
- Configurable API endpoints via `App.config`

## Getting Started

### Prerequisites

- Visual Studio 2026  
- .NET 10 SDK  
- The REST service `OctobridgeCoreRestService` running in IIS Express or IIS  

### Configuration

Edit `appsettings.json` to set the correct API endpoints:

```json
  "AppSettings": {
    "productsURI": "api/v1/products",
    "rootURI": "https://localhost:44366/"
  }
```

### Running the Application

1. Start the `OctobridgeCoreRestService` in IIS Express (within Visual Studio) or IIS.
2. Build and run the `DataMaintenanceEnhanced` WPF application.
3. Perform data operations through the application's UI, which communicates with the REST service.

## Testing

- Ensure the REST service is running before launching the application.
- Use the application's UI to verify data operations (CRUD) are functioning as expected.
