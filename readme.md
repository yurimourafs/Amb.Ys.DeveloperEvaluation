# Ambev Developer Evaluation Solution

This repository contains a multi-project .NET 8 solution designed for a complete developer evaluation scenario. The solution includes API, domain, application, dependency injection, and testing projects, as well as Docker Compose support for local development with PostgreSQL, MongoDB, and Redis.

---

## Solution Structure

### 1. **Ambev.DeveloperEvaluation.WebApi**
- **Type:** ASP.NET Core Web API
- **Purpose:** Exposes RESTful endpoints for managing Users, Sales, SaleItems, authentication, and more.
- **Features:** JWT authentication, Swagger UI, Serilog logging (with MongoDB sink), validation middleware, and integration with the application layer.

### 2. **Ambev.DeveloperEvaluation.Application**
- **Type:** .NET Class Library
- **Purpose:** Implements business logic, CQRS handlers (MediatR), validation, and mapping profiles.
- **Features:** Command/query handlers for CRUD operations, validation with FluentValidation, and AutoMapper profiles.

### 3. **Ambev.DeveloperEvaluation.Domain**
- **Type:** .NET Class Library
- **Purpose:** Contains core domain entities, enums, validation logic, and repository interfaces.
- **Features:** Domain-driven design, entity validation, and business rules.

### 4. **Ambev.DeveloperEvaluation.ORM**
- **Type:** .NET Class Library
- **Purpose:** Entity Framework Core mappings and repository implementations.
- **Features:** Database context, entity configurations, and repository classes for persistence.

### 5. **Ambev.DeveloperEvaluation.IoC**
- **Type:** .NET Class Library
- **Purpose:** Dependency injection setup for all layers.
- **Features:** Registers services, repositories, handlers, and other dependencies.

### 6. **Ambev.DeveloperEvaluation.Unit**
- **Type:** xUnit Test Project
- **Purpose:** Unit tests for domain and application logic.
- **Features:** Tests for validators, handlers, and domain entities.

### 7. **Ambev.DeveloperEvaluation.Integration**
- **Type:** xUnit Test Project
- **Purpose:** Integration tests for API endpoints and database interactions.
- **Features:** End-to-end tests using WebApplicationFactory, Flurl.Http, and real database containers.

---

## Running the Solution with Docker

The solution uses Docker Compose to orchestrate the following services:
- **Web API** (`ambev.developerevaluation.webapi`)
- **PostgreSQL** (`ambev.developerevaluation.database`)
- **MongoDB** (`ambev.developerevaluation.nosql`)
- **Redis** (`ambev.developerevaluation.cache`)

### 1. **Prerequisites**
- Docker and Docker Compose installed
- .NET 8 SDK installed (for local builds and tests)

### 2. **Start the Environment**

From the root of the repository, run:
docker-compose up

This will:
- Build and start the Web API on port `8080`
- Start PostgreSQL on port `5432`
- Start MongoDB on port `27017` (with user: `developer`, password: `ev@luAt10n`)
- Start Redis on port `6379`

### 3. **Accessing the API**

- The API will be available at: [http://localhost:8080](http://localhost:8080)
- Swagger UI: [http://localhost:8080/swagger](http://localhost:8080/swagger)

### 4. **Default Credentials**

For authentication in tests and Swagger:
- **Email:** `admin@email.com`
- **Password:** `Admin@123`

---

## Running Tests

### 1. **Unit Tests**

Run from the solution root:
dotnet test tests/Ambev.DeveloperEvaluation.Unit

### 2. **Integration Tests**

Run from the solution root:
dotnet test tests/Ambev.DeveloperEvaluation.Integration

Integration tests will authenticate, interact with the API, and verify real database operations.

---

## Logging

- **Serilog** is configured to log to both console and MongoDB (`logsdb` database, `applogs` collection).
- MongoDB credentials are set in `appsettings.json` and `docker-compose.yml`.

---

## Additional Notes

- All projects target `.NET 8`.
- Environment variables and connection strings are managed via `appsettings.json` and Docker Compose.
- For development, you can run the API locally with `dotnet run` and connect to local or containerized databases.

---

## Troubleshooting

- If you encounter database connection issues, ensure containers are healthy and ports are not blocked.
- For authentication errors, verify the default user exists in the database.
- For MongoDB logging, check the `logsdb` database and `applogs` collection for log entries.

---

## Contributing

Feel free to fork, open issues, or submit pull requests for improvements or bug fixes.

---

**Enjoy developing and testing with the Ambev Developer Evaluation Solution!**