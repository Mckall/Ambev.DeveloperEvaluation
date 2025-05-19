# Ambev Developer Evaluation

This project is a comprehensive developer evaluation system designed to assess backend development skills through a real-world application scenario. It simulates a sales system incorporating business rules, domain-driven design principles, and modern architectural patterns.

## Features

- **Sales Management**: Complete handling of sales processes, including business rules and event-driven operations.
- **Shopping Cart**: CRUD operations to add, update, and remove items before finalizing a sale.
- **Branches and Products**: Registration and management to support sales operations.
- **Users and Customers**: User registration, authentication, and profile management.
- **Messaging**: Asynchronous communication with external systems using RabbitMQ.
- **Caching and Database**: Integration with Redis for caching and PostgreSQL for data persistence.
- **Authentication**: Secure system based on JWT tokens.

## Technologies Used

- .NET 8
- Docker & Docker Compose
- RabbitMQ
- PostgreSQL
- Redis
- FluentValidation
- MediatR
- xUnit & FluentAssertions for testing

## Prerequisites

- .NET 8 SDK or higher
- Docker and Docker Compose installed
- An IDE or code editor of your choice (e.g., Visual Studio, VS Code)

## Getting Started

1. **Clone the repository**:

   ```bash
   git clone https://github.com/Mckall/Ambev.DeveloperEvaluation.git
   cd Ambev.DeveloperEvaluation
   ```

2. **Build and run the application using Docker Compose**:

   ```bash
   docker-compose up --build
   ```

   This command will set up the necessary services, including the web API, PostgreSQL, Redis, and RabbitMQ.

3. **Apply database migrations**:

   Ensure the PostgreSQL service is running, then apply migrations to set up the database schema.

   ```bash
   dotnet ef database update
   ```

4. **Seed initial data**:

   Locate the `SaleSeed.sql` script in the `/Adapters/Drivers/Infrastructure/Ambev.DeveloperEvaluation.ORM/` directory and execute it against your PostgreSQL database to insert initial data such as categories, products, and companies.

## Running Tests

The project includes unit and integration tests to ensure code quality and functionality. To run the tests:

```bash
dotnet test
```

## Project Structure

- **src/**: Contains the main application code, organized by domains and layers following DDD principles.
- **tests/**: Contains test projects for unit and integration testing.
- **Dockerfile**: Defines the Docker image for the application.
- **docker-compose.yml**: Defines the services required to run the application in a containerized environment.

## API Endpoints

The application exposes several API endpoints for managing sales, products, customers, and more. For detailed information on available endpoints, request and response formats, and usage examples, please refer to the API documentation or use tools like Swagger UI if integrated.

## Business Rules

The application enforces specific business rules, such as:

- Purchases of more than 4 identical items receive a 10% discount.
- Purchases between 10 and 20 identical items receive a 20% discount.
- It's not possible to sell more than 20 identical items.
- Purchases of fewer than 4 items do not receive a discount.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.