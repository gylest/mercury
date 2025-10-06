# Angular.Client

This project is an Angular application that serves as the client-side interface for managing customers, orders, products, and attachments.  
It is designed to work with a .NET backend and is structured for maintainability and scalability.  

## Product Features

- Manage customers, orders, products, and attachments
- Edit existing customers, orders, and products
- Responsive navigation and error handling
- Integration with a .NET backend API

## Architecture Features

- **Standalone Components**  
  All UI components are implemented as standalone components. This means each component directly imports its own dependencies and can be used independently, without needing to be declared in an NgModule. This modern Angular approach improves modularity, reusability, and simplifies the application structure.

- **Feature-Based Organization**  
  The project is organized by feature, with separate directories for managing and editing customers, orders, products, and attachments. This structure makes the codebase easier to navigate, maintain, and scale.

- **Centralized Routing**  
  Application routes are defined in a single routing configuration file (`app.routing.ts`). Each route maps directly to a standalone component, enabling clear and maintainable navigation throughout the application.

- **Service-Oriented Data Access**  
  All communication with the backend API is handled through Angular services. This separation of concerns keeps business logic out of UI components and promotes testability and maintainability.

- **Material Design Integration**  
  Angular Material is used for UI components and styling, providing a consistent and modern user interface across the application.

- **HTTP Interceptors**  
  HTTP interceptors are used to manage API base URLs and handle errors globally, ensuring consistent request formatting and user feedback.

- **Strict TypeScript Configuration**  
  The project uses strict TypeScript and Angular compiler options, enforcing best practices and reducing the likelihood of runtime errors.

- **Testing Support**  
  Unit testing is set up using Jasmine and Karma, with all `*.spec.ts` files automatically included in the test suite.

---

## Project Structure

- `src/app/` - Main application source code
- `src/app/services/` - Angular services for API communication
- `src/app/manage-*` - Feature components for managing entities
- `src/app/edit-*` - Components for editing entities

## Application Routes

The application defines the following main routes:

| Route Path            | Component                   | Description                                 |
|---------------------- |-----------------------------|---------------------------------------------|
| `/`                   | `HomeComponent`             | Home page of the application                |
| `/manage-attachments` | `ManageAttachmentsComponent`| Manage and view attachments                 |
| `/manage-customers`   | `ManageCustomersComponent`  | Manage and view customers                   |
| `/manage-orders`      | `ManageOrdersComponent`     | Manage and view orders                      |
| `/manage-products`    | `ManageProductsComponent`   | Manage and view products                    |

### Additional Routes

- `/edit-order`      → `EditOrderComponent` (Edit an order)
- `/edit-customer`   → `EditCustomerComponent` (Edit a customer)
- `/edit-product`    → `EditProductComponent` (Edit a product)
- `**` (wildcard)    → `HomeComponent` (Fallback for unknown routes)

## Getting Started

1. Install dependencies: `npm install`  
2. Check for package updates: `npm run upgrade`  
3. Lint the code: `ng lint`  
4. Build the application: `ng build`  
5. Run unit tests: `ng test`  
6. Run the application: `ng serve`  
7. Open your browser and navigate to <http://localhost:63498/>.  
8. Alternatively run the backend and frontend together using the provided solution file in Visual Studio. This can be achieved by starting the configuration `Angular Profile`.  

## Important Files

Below are key files in the `Angular.Client` project and their purposes:

- **angular.json**  
  The main Angular CLI configuration file. It defines project structure, build and serve options, assets, styles, scripts, environments, and test configurations.

- **app.component.ts**  
  The root component of the Angular application. It acts as the main container for the app’s UI and typically includes the navigation and router outlet for displaying routed views.

- **app.module.ts**  
  The root Angular module (NgModule) that declares and imports all components, modules, and services needed for the application. It is the entry point for Angular’s dependency injection and bootstrapping (not used if the project is fully standalone).

- **app.routing.ts**  
  Defines the application's route configuration. It maps URL paths to Angular components, enabling navigation between different views such as home, manage-customers, manage-orders, etc.

- **karma.conf.js**  
  The configuration file for the Karma test runner. It specifies how unit tests are run, which files to include, which browsers to use, and how results are reported.  
  Any file ending with .spec.ts (such as manage-orders.component.spec.ts) located anywhere under src/ (e.g., src/app/) will be picked up and run as a unit test by Karma.  

- **material.module.ts**  
  A custom Angular module that imports and exports Angular Material components used throughout the application, making Material UI components available in other modules.

- **package.json**  
  Lists all npm dependencies, scripts, and metadata for the project. It is essential for managing third-party libraries, build scripts, and project information.

- **proxy.conf.js**  
  Configures a development-time proxy for API requests. It allows the Angular dev server to forward certain requests (e.g., `/api`) to the backend server, helping to avoid CORS issues during development.

- **tsconfig.json**  
  The TypeScript configuration file. It defines compiler options, file inclusions, strictness settings, and Angular-specific compiler options for the project.

---

## Notes

- The application expects a backend API running (see backend documentation for setup).  
- API base URLs and ports may need to be configured in the environment files or HTTP interceptors.  
- Ensure CORS is properly configured on the backend to allow requests from the Angular client.  
- The application uses Angular Material for UI components and styling.  
- Error handling is implemented to provide user feedback on API request failures.  
- The project is structured to facilitate easy addition of new features and components.  
- Unit tests can be added using Jasmine and Karma (not included in this version).  
- For production builds, use `ng build --prod` to optimize the application.  
- Consider using Angular CLI for generating new components and services to maintain consistency.  

