# WeCommerce
WeCommerce is a mock eCommerce website that allows users to browse products


## Installation
Requirements:
* Dotnet SDK 8.0+
* Entity Framework Core Tools (dotnet tool install --global dotnet-ef)
* MSSQL Server 2022 

### Database
To create/update the database, you must:
1. Configure appsettings.json with your connection string

2. Run all migrations:

#### Nuget Package Manager Console
Run `update-database` to run the migrations

#### Dotnet CLI
Run `dotnet ef database update` to run the migrations

### Running
Use either Visual Studio or `dotnet run`

> [!WARNING]  
> The app creates a default admin user with the username `admin` and the password `password`. 
> You MUST change this password after login.