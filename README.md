## API End points 
## Amenity Endpoints

| HTTP Method | Endpoint                    | Description                     |
|-------------|-----------------------------|---------------------------------|
| GET         | `/api/amenity/{id}`          | Get an amenity by ID             |
| GET         | `/api/amenity/amenity-search`| Retrieve a filtered list of amenities |
| POST        | `/api/amenity`               | Create a new amenity             |
| PUT         | `/api/amenity/{id}`          | Update an existing amenity by ID |
| DELETE      | `/api/amenity/{id}`          | Delete an amenity by ID          |


## Booking Endpoints

| HTTP Method | Endpoint                    | Description                     |
|-------------|-----------------------------|---------------------------------|
| GET         | `/api/booking/{id}`          | Get a booking by ID             |
| GET         | `/api/booking/booking-search`| Retrieve filtered bookings      |
| POST        | `/api/booking`               | Create a new booking            |
| PUT         | `/api/booking/{id}`          | Update an existing booking by ID |
| DELETE      | `/api/booking/{id}`          | Delete a booking by ID          |
| POST        | `/api/booking/bookroom`      | Book a room and add it to cart  |


## CartItem Endpoints

| HTTP Method | Endpoint                    | Description                     |
|-------------|-----------------------------|---------------------------------|
| GET         | `/api/cartitem/{id}`         | Get a cart item by ID           |
| GET         | `/api/cartitem/cartitem-search` | Retrieve filtered cart items  |
| POST        | `/api/cartitem`              | Create a new cart item          |
| PUT         | `/api/cartitem/{id}`         | Update an existing cart item by ID |
| DELETE      | `/api/cartitem/{id}`         | Delete a cart item by ID        |


## Discount Endpoints

| HTTP Method | Endpoint                    | Description                     |
|-------------|-----------------------------|---------------------------------|
| GET         | `/api/discount/{id}`         | Get a discount by ID            |
| GET         | `/api/discount/discount-search` | Retrieve filtered discounts  |
| POST        | `/api/discount`             | Create a new discount            |
| PUT         | `/api/discount/{id}`         | Update an existing discount by ID |
| DELETE      | `/api/discount/{id}`         | Delete a discount by ID         |



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

