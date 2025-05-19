# OrdersTracker

OrdersTracker is a sample project designed to manage customer orders. It includes an API, a Database Deployment Project, an MVC application for managing and viewing customer data and orders, and Contracts project contains shared models used across the application.

## Features

- **API**: Provides endpoints for getting information for the customers and their orders.
- **MVC Application**: A web interface for interacting with customer and order data.
- **Database Deployment**: Scripts and tools for setting up the database.
- **Integration Tests**: Ensures the functionality of the API and MVC application.

## Prerequisites

To run this project locally, ensure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/) (optional, if you want to run SQL Server in Docker)
- [SQL Server 2019](https://www.microsoft.com/en-us/evalcenter/download-sql-server-2019)

## Quick Startup - Running the Project Locally

### Option 1. Clone the Repository

```bash
git clone https://github.com/Ivkoto/OrdersTracker.git
```

### Option 2. Unrar the project from the zip file

### 2. Set Up the Database

To set up the database, you can use SQL Server 2019. If you want to run SQL Server using Docker, use the following command to download and start the SQL Server 2019 (latest) container:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 --name sqlserver2019 -d mcr.microsoft.com/mssql/server:2019-latest
```

**If you are running SQL Server locally without Docker, ensure it is installed and running on `localhost:1433`.**

### 3. Run the Database Deploy

This step will create the database, execute the scripts to create all the tables and procedures, and insert the test data.

1. Navigate to the project directory:
   ```bash
   cd OrdersTracker
   ```
2. Run the Databse Deploy:
   ```bash
   dotnet run --project OrdersTracker.Database.Deploy
   ```

### 3. Run the API

1. Navigate to the project directory:
   ```bash
   cd OrdersTracker
   ```
2. Run the API:
   ```bash
   dotnet run --project OrdersTracker.API --launch-profile https
   ```
3. The API will be available at both `https://localhost:7179` and `http://localhost:5122`.

### 4. Run the MVC Application

1. Navigate to the project directory:
   ```bash
   cd OrdersTracker
   ```
2. Run the MVC application:
   ```bash
   dotnet run --project OrdersTracker.MVC --launch-profile https
   ```
3. The application will be available at both `https://localhost:7092` and `http://localhost:5054`.

## Running Tests

### Integration Tests

1. Navigate to the project directory:
   ```bash
   cd OrdersTracker
   ```
2. Run the tests:
   ```bash
   dotnet test OrdersTracker.IntegrationTests
   ```

## Debugging Utilities

This project includes a `DeleteBinObjFolders.bat` file, which is used for debugging purposes. It allows you to quickly delete all `bin` and `obj` folders in the project to ensure a clean build environment. Just double click to run it.

## License

This project is licensed under the MIT License. See the [`LICENSE`](./LICENSE) file for details.
