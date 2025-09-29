# Quick Start Guide

Get the TutorialAPI running in under 3 minutes.

## Prerequisites Check

Verify you have the required tools installed:

```bash
# Check .NET 8 SDK
dotnet --version
# Should show: 8.x.x

# Check Docker Desktop is running
docker --version
docker compose --version
```

## Step-by-Step Setup

### 1. Clone and Navigate
```bash
git clone <your-repo-url>
cd TutorialAPI
```

### 2. Start Database Infrastructure
```bash
# Start PostgreSQL and Redis containers
docker compose up -d

# Verify containers are running
docker ps
# You should see: user-microservices-db and user-microservices-redis
```

### 3. Setup Database Schema
```bash
# Create and seed the database
dotnet ef database update --project src/UserCore --startup-project src/UserApi
```

### 4. Run the API
```bash
# Navigate to API project
cd src/UserApi

# Start the application
dotnet run
```

### 5. Verify Everything Works
- ✅ API: http://localhost:5195
- ✅ Swagger: http://localhost:5195/swagger
- ✅ Test endpoint: http://localhost:5195/users

## Common Issues & Solutions

### Database Connection Refused
**Problem:** `Failed to connect to 127.0.0.1:5432`
**Solution:** 
```bash
# Restart Docker containers
docker compose down
docker compose up -d
```

### Missing Database Tables
**Problem:** `relation "Users" does not exist`
**Solution:**
```bash
dotnet ef database update --project src/UserCore --startup-project src/UserApi
```

### Port Already in Use
**Problem:** `Address already in use`
**Solution:**
```bash
# Find and kill process using port 5195
lsof -ti:5195 | xargs kill -9
```

## Reset Everything
If something goes wrong, reset the entire environment:

```bash
# Stop API (Ctrl+C)
# Remove containers and data
docker compose down -v
# Restart fresh
docker compose up -d
dotnet ef database update --project src/UserCore --startup-project src/UserApi
cd src/UserApi && dotnet run
```

## Next Steps
- Check out the full [README.md](README.md) for architecture details
- Explore the [API endpoints](README.md#api-endpoints)
- View the Swagger documentation at `/swagger`