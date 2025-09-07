# TutorialAPI - .NET 8 Microservices

A simple microservices application built with .NET 8 to demonstrate modern web API development practices.

## Architecture

This project implements a microservices architecture with three main components:

- **UserApi** - REST API service providing user and role management endpoints
- **UserCore** - Shared library containing domain models and Entity Framework DbContext
- **UserJobs** - Background worker service for asynchronous processing

## Technologies Used

- .NET 8
- ASP.NET Core Web API (Minimal APIs)
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose
- Redis
- Swagger/OpenAPI

## Project Structure

```
├── src/
│   ├── UserApi/           # REST API endpoints
│   ├── UserCore/          # Domain models and data access
│   └── UserJobs/          # Background worker service
├── docker-compose.yml     # PostgreSQL and Redis containers
└── UserMicroservices.sln  # Solution file
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker Desktop

### Running the Application

1. **Start the infrastructure services:**
   ```bash
   docker compose up -d
   ```

2. **Run database migrations:**
   ```bash
   dotnet ef database update --project src/UserCore --startup-project src/UserApi
   ```

3. **Start the API:**
   ```bash
   dotnet run --project src/UserApi
   ```

4. **Access the API:**
   - API Base URL: `http://localhost:5195`
   - Swagger UI: `http://localhost:5195/swagger`

## API Endpoints

- `GET /users` - Retrieve all users
- `GET /roles` - Retrieve all roles

## Database

The application uses PostgreSQL with the following entities:
- **Users** - User accounts with email and personal information
- **Roles** - User roles (Admin, User, etc.)
- **UserRoles** - Many-to-many relationship between users and roles

Default seed data includes Admin and User roles.

## Development

This project was built as a learning exercise to understand:
- Microservices architecture patterns
- .NET 8 Minimal APIs
- Entity Framework Core with PostgreSQL
- Docker containerization
- Modern API documentation with Swagger

## Built With

- Visual Studio Code
- Entity Framework Core Tools
- Docker Desktop
- DataGrip (database client)
