
# Ocelot MicroService

This project demonstrates the implementation of an API Gateway using Ocelot in .NET 8. It leverages both **InMemory DB** and **SQL Server** for storage, and includes a **Dockerfile** for containerization.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Setup](#setup)
  - [Prerequisites](#prerequisites)
  - [Running Locally](#running-locally)
  - [Docker Setup](#docker-setup)
- [Database Setup](#database-setup)
  - [InMemory DB](#inmemory-db)
  - [SQL Server](#sql-server)
  - [CodeFirst with .NET CLI](#codefirst-with-dotnet-cli)
- [License](#license)

---

## Overview

This project uses **Ocelot** as an API Gateway to route API requests to various microservices. It demonstrates how to set up an API Gateway in a microservices architecture, implementing routing, load balancing, and API aggregation.  
For full documentation, visit: [Ocelot Documentation](https://ocelot.readthedocs.io/en/latest/).

### Ocelot API Gateway
- **Routing**: Routes requests to backend services based on predefined routes in the `ocelot.json` configuration file.
- **Load Balancing**: Routes traffic to multiple instances of backend services if needed.
- **Aggregation**: Aggregates multiple responses into a single response.

The project also uses an **InMemory Database** for temporary data storage and **SQL Server** for persistent data storage.

## Features

- **Ocelot API Gateway** routing and request aggregation
- In-memory data storage for fast temporary data operations
- SQL Server for persistent data storage
- Docker support for easy containerization and deployment
- Scalable architecture with microservices communication via the API Gateway

## Tech Stack

- **.NET 8**
- **Ocelot API Gateway**
- **InMemory Database** (for temporary data storage)
- **SQL Server** (for persistent storage)
- **Docker** (for containerization)

---

## Setup

### Prerequisites

To run this project, ensure you have the following installed on your machine:

- .NET 8 SDK
- Docker
- SQL Server (for persistent database) or use SQL Server Docker container

### Running Locally

1. Clone the repository:

   ```bash
   git clone https://github.com/tasbilek/OcelotMicroService.git
   cd OcelotMicroService
   ```

2. Restore the dependencies:

   ```bash
   dotnet restore
   ```

3. Update the connection strings in the `appsettings.json` for **SQL Server** configuration.

4. Run the application:

   ```bash
   dotnet run
   ```

The API Gateway will be available locally at `http://localhost:5000`.

### Docker Setup

This project includes a **Dockerfile** for containerization. You can build and run the project using Docker.

1. **Build the Docker image**:

   ```bash
   docker build -t todos-img -f OcelotMicroService.Todos.WebAPI/Dockerfile .
   docker build -t categories-img -f OcelotMicroService.Categories.WebAPI/Dockerfile .
   ```

2. **Run the Docker container**:

   ```bash
   docker run -d --name todos-api -p 5001:8080 todos-img
   docker run -d --name categories-api -p 5002:8080 categories-img
   ```

3. If you want, you can dockerize the `Ocelot.Gateway` project; otherwise, you need to run it manually.

4. To run SQL Server in Docker (if not already running locally), you can find it this address:
   https://hub.docker.com/r/microsoft/azure-sql-edge

---

## Database Setup

### InMemory DB

For temporary data storage, the project uses **InMemory DB**. You can use this for development or testing purposes when you don't need persistent storage.

### SQL Server

For persistent data storage, **SQL Server** is used. Ensure that the SQL Server container or your local SQL Server instance is running. You can configure the connection strings in the `appsettings.json` file for **SQL Server**.

If you are using a **SQL Server container**, the connection string should be configured as follows:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=host.docker.internal,1433;Database=YourDatabase;User Id=sa;Password=yourPassword123!"
}
```

- `host.docker.internal` allows the container to communicate with your local machine.
- `1433` is the default port for SQL Server.
- Replace `YourDatabase` with your actual database name, and use the appropriate `sa` password.

On Mac (and also on some Linux environments), host.docker.internal might not work as expected to refer to the host machine. In such cases, you can use your local machine's IP address instead.

To find your local machine's IP address on Mac, you can use the following command in the terminal:

```bash
ifconfig | grep inet
```

Look for the IP address associated with your network interface (typically en0 for Wi-Fi or en1 for Ethernet). Use this IP address in place of `host.docker.internal` in your connection string.

For example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=192.168.x.x,1433;Database=YourDatabase;User Id=sa;Password=yourPassword123!"
}
```
Where `192.168.x.x` is the IP address of your local machine.

This approach should work in environments where `host.docker.internal` is not resolving correctly.

If you're running SQL Server locally, you can adjust the connection string to point to your local instance.

---

### CodeFirst with .NET CLI

1. Create a migration to create the database schema from your models:

```bash
dotnet ef migrations add InitialCreate
```
2. Apply the migration to the database:

```bash
dotnet ef database update
```
---

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).
