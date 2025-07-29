## Architecture Overview

This project follows the principles of **Clean Architecture**, ensuring separation of concerns and high maintainability. It is organized into distinct layers:

### Layers:

- **Domain**  
  Contains the core business logic and domain entities. This layer is independent of any external frameworks or technologies.

- **Application**  
  Defines application-specific logic such as service interfaces, use cases, DTOs, and commands/queries. Depends only on the Domain layer.

- **Infrastructure**  
  Implements the actual data access logic, external APIs, authentication providers, and other services. Depends on the Application and Domain layers.

- **Web API (Presentation)**  
  Exposes the application to the outside world via RESTful endpoints. Handles HTTP requests, controllers, middleware, and routing.

### Benefits:

- Testable core logic (Domain and Application)
- Low coupling between layers
- High scalability and flexibility
- Easy to replace or upgrade infrastructure components without affecting business logic

### Folder Structure Example:

/src
├── Domain
├── Application
├── Infrastructure
└── WebAPI

This structure promotes a clean separation of responsibilities and supports long-term project maintainability and scalability.


## Technology

This project is built with the following technologies:

- **.NET 8 / ASP.NET Core Web API** — Backend framework for building RESTful services
- **Entity Framework Core** — ORM for database access
- **SQL Server** — Relational database for storing booking, user, and accommodation data
- **xUnit** — Unit testing framework for .NET
- **Swagger / Swashbuckle** — For interactive API documentation
- **MediatR** — For implementing CQRS pattern with clean separation of concerns
- **AutoMapper** — For mapping between entities and DTOs
- **JWT Authentication** — For securing APIs with token-based authentication
- **Rate Limiting Middleware** — To prevent abuse and control traffic
- **FluentValidation** — For validating incoming requests
- **Clean Architecture** — For structured, maintainable, and testable codebase

Optional:
- **Docker** — Containerization for development or deployment (if added)
- **Serilog** — Logging framework (if configured)


## Testing

- **Unit Tests**  
  - Test individual components and services in the Domain and Application layers.
  - Ensure business rules and logic work correctly in isolation.

- **Integration Tests**  
  - Test API endpoints and their integration with the database and services.
  - Verify end-to-end workflows and data flow.

- **Testing Framework**  
  - Uses **xUnit** for writing and running tests.
  - Mock dependencies with libraries such as **Moq** (if used).


 ## Contact & Support

If you have any questions, suggestions, or need help, feel free to reach out:

- **Maintainer:** Tariq Omar  
- **Email:** tariqomar2004@gmail.com  
- **GitHub:** [Tariq0mar](https://github.com/Tariq0mar)

You can also open issues or feature requests on the GitHub repository.

Thank you for using the Travel-and-Accommodation-Booking-Platform!

