# Travel-and-Accommodation-Booking-Platform

## Overview

This project is a comprehensive RESTful API designed to manage various entities related to a hospitality or travel booking system. It provides endpoints to handle operations on galleries, hotels, rooms, amenities, discounts, users, payments, reviews, and locations.

The API supports full CRUD (Create, Read, Update, Delete) functionality with filtered search capabilities and specialized endpoints for detailed queries such as visitor statistics and user-specific reviews. Authentication and authorization are integrated to secure sensitive operations.

This solution is ideal for building backend services for hotel booking platforms, travel agencies, or hospitality management systems, offering extensibility and scalability through a clean, organized architecture.


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



## Gallery Endpoints

| HTTP Method | Endpoint                     | Description                  |
|-------------|------------------------------|------------------------------|
| GET         | /api/gallery/{id}             | Get a gallery item by ID      |
| GET         | /api/gallery/gallery-search  | Retrieve filtered gallery items |
| POST        | /api/gallery                 | Create a new gallery item     |
| PUT         | /api/gallery/{id}            | Update a gallery item by ID   |
| DELETE      | /api/gallery/{id}            | Delete a gallery item by ID   |

## HotelAmenity Endpoints

| HTTP Method | Endpoint                           | Description                     |
|-------------|----------------------------------|---------------------------------|
| GET         | /api/hotelamenity/{id}            | Get a hotel amenity by ID        |
| GET         | /api/hotelamenity/hotelamenity-search | Retrieve filtered hotel amenities |
| POST        | /api/hotelamenity                | Create a new hotel amenity       |
| PUT         | /api/hotelamenity/{id}           | Update a hotel amenity by ID     |
| DELETE      | /api/hotelamenity/{id}           | Delete a hotel amenity by ID     |

## Hotel Endpoints

| HTTP Method | Endpoint                       | Description                 |
|-------------|--------------------------------|-----------------------------|
| GET         | /api/hotel/{id}                | Get a hotel by ID           |
| GET         | /api/hotel/FullDetails/{id}   | Get full details of a hotel by ID |
| GET         | /api/hotel/hotel-search       | Retrieve filtered hotels    |
| POST        | /api/hotel                   | Create a new hotel          |
| PUT         | /api/hotel/{id}               | Update a hotel by ID        |
| DELETE      | /api/hotel/{id}               | Delete a hotel by ID        |

## HotelDiscount Endpoints

| HTTP Method | Endpoint                        | Description                     |
|-------------|---------------------------------|---------------------------------|
| GET         | /api/hoteldiscount/{id}          | Get a hotel discount by ID       |
| GET         | /api/hoteldiscount/hoteldiscount-search | Retrieve filtered hotel discounts |
| POST        | /api/hoteldiscount             | Create a new hotel discount      |
| PUT         | /api/hoteldiscount/{id}         | Update a hotel discount by ID    |
| DELETE      | /api/hoteldiscount/{id}         | Delete a hotel discount by ID    |


## Location Endpoints

| HTTP Method | Endpoint                          | Description                                   |
|-------------|----------------------------------|-----------------------------------------------|
| GET         | /api/location/{id}                | Get location by ID                             |
| GET         | /api/location/location-search    | Retrieve filtered locations                    |
| GET         | /api/location/city-visitors-details | Get visitors details grouped by city and year |
| POST        | /api/location                    | Create a new location                          |
| PUT         | /api/location/{id}               | Update location by ID                          |
| DELETE      | /api/location/{id}               | Delete location by ID                          |

## Login Endpoint

| HTTP Method | Endpoint       | Description                 |
|-------------|----------------|-----------------------------|
| POST        | /api/Login     | User login, returns a token |

## Payment Endpoints

| HTTP Method | Endpoint                 | Description                |
|-------------|--------------------------|----------------------------|
| GET         | /api/payment/{id}        | Get payment by ID          |
| GET         | /api/payment/payment-search | Retrieve filtered payments |
| POST        | /api/payment             | Create a new payment       |
| PUT         | /api/payment/{id}        | Update payment by ID       |
| DELETE      | /api/payment/{id}        | Delete payment by ID       |

## Review Endpoints

| HTTP Method | Endpoint                 | Description                                   |
|-------------|--------------------------|-----------------------------------------------|
| GET         | /api/review/my-reviews   | Get reviews of the currently logged-in user  |
| GET         | /api/review/{id}         | Get review by ID (Admin only)                  |
| GET         | /api/review/review-search | Retrieve filtered reviews (Admin only)        |
| POST        | /api/review              | Create a new review (Authorized users)        |
| PUT         | /api/review/{id}         | Update review by ID (Admin only)               |
| DELETE      | /api/review/{id}         | Delete review by ID (Admin only)               |



## RoomAmenity Endpoints

| HTTP Method | Endpoint                        | Description              |
|-------------|--------------------------------|--------------------------|
| GET         | /api/roomamenity/{id}           | Get room amenity by ID    |
| GET         | /api/roomamenity/roomamenity-search | Get filtered room amenities |
| POST        | /api/roomamenity               | Create a new room amenity |
| PUT         | /api/roomamenity/{id}          | Update room amenity by ID |
| DELETE      | /api/roomamenity/{id}          | Delete room amenity by ID |

## RoomCategory Endpoints

| HTTP Method | Endpoint                        | Description               |
|-------------|--------------------------------|---------------------------|
| GET         | /api/roomcategory/{id}          | Get room category by ID    |
| GET         | /api/roomcategory/roomcategory-search | Get filtered room categories |
| POST        | /api/roomcategory              | Create a new room category |
| PUT         | /api/roomcategory/{id}         | Update room category by ID |
| DELETE      | /api/roomcategory/{id}         | Delete room category by ID |

## RoomCategoryDiscount Endpoints

| HTTP Method | Endpoint                               | Description                   |
|-------------|---------------------------------------|-------------------------------|
| GET         | /api/roomcategorydiscount/{id}        | Get room category discount by ID |
| GET         | /api/roomcategorydiscount/roomcategorydiscount-search | Get filtered discounts          |
| POST        | /api/roomcategorydiscount             | Create a new room category discount |
| PUT         | /api/roomcategorydiscount/{id}        | Update discount by ID          |
| DELETE      | /api/roomcategorydiscount/{id}        | Delete discount by ID          |

## Room Endpoints

| HTTP Method | Endpoint                    | Description                    |
|-------------|-----------------------------|-------------------------------|
| GET         | /api/room/{id}              | Get room by ID                |
| GET         | /api/room/room-search       | Get filtered rooms            |
| GET         | /api/room/FullDetails/{id}  | Get full details of room by ID |
| POST        | /api/room                  | Create a new room             |
| PUT         | /api/room/{id}             | Update room by ID             |
| DELETE      | /api/room/{id}             | Delete room by ID             |


## RoomGallery Endpoints

| HTTP Method | Endpoint                         | Description                |
|-------------|---------------------------------|----------------------------|
| GET         | /api/roomgallery/{id}            | Get RoomGallery by ID      |
| GET         | /api/roomgallery/roomgallery-search | Get filtered RoomGalleries  |
| POST        | /api/roomgallery                | Create a new RoomGallery   |
| PUT         | /api/roomgallery/{id}           | Update RoomGallery by ID   |
| DELETE      | /api/roomgallery/{id}           | Delete RoomGallery by ID   |

## User Endpoints

| HTTP Method | Endpoint                   | Description             |
|-------------|----------------------------|-------------------------|
| GET         | /api/user/{id}             | Get User by ID          |
| GET         | /api/user/user-search      | Get filtered Users      |
| POST        | /api/user                 | Create a new User       |
| PUT         | /api/user/{id}            | Update User by ID       |
| DELETE      | /api/user/{id}            | Delete User by ID       |

## UserDiscount Endpoints

| HTTP Method | Endpoint                         | Description                |
|-------------|---------------------------------|----------------------------|
| GET         | /api/userdiscount/{id}           | Get UserDiscount by ID     |
| GET         | /api/userdiscount/userdiscount-search | Get filtered UserDiscounts |
| POST        | /api/userdiscount               | Create a new UserDiscount  |
| PUT         | /api/userdiscount/{id}          | Update UserDiscount by ID  |
| DELETE      | /api/userdiscount/{id}          | Delete UserDiscount by ID  |



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

