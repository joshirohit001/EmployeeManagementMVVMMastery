# Employee Management MVVM

A beginner-friendly WPF desktop application built with .NET 8, C#, MVVM, SQL Server and Entity Framework Core.

## Features

- Create, read, update and delete employees
- Search by employee name, email or department
- TextBox, ComboBox, RadioButton, CheckBox, DatePicker, ListBox and DataGrid
- Form validation and duplicate email checking
- EF Core SQL Server persistence
- Repository and service layers
- Dependency injection
- Asynchronous database operations

## Requirements

- Visual Studio 2022 with the .NET desktop development workload
- .NET 8 SDK
- SQL Server LocalDB (installed with many Visual Studio workloads) or SQL Server

## Run the application

1. Extract the ZIP file.
2. Open `EmployeeManagementMVVM.csproj` in Visual Studio.
3. Restore NuGet packages.
4. Check the connection string in `App.xaml.cs`.
5. Press F5.

Default connection string:

`Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;`

Change it if you use a different SQL Server instance, for example:

`Server=localhost;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;`

## Create the database

The application uses EF Core migrations. Open **Tools > NuGet Package Manager > Package Manager Console** and run:

```powershell
Add-Migration InitialCreate
Update-Database
```

If the EF command is not found, install the EF Core tools:

```powershell
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.11
```

Alternatively, from a terminal in the project folder:

```bash
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Notes

- The project targets `net8.0-windows`.
- The database is created by applying the migration; it is not created merely by opening the application.
- The default connection string uses SQL Server LocalDB.
- Email is unique in the database. If an email already exists, the service shows a validation error.
- The code keeps the UI simple. The two small code-behind handlers only transfer RadioButton and multi-select ListBox selections into ViewModel properties. Main application actions use commands.
- This is a learning starter project. For production, add stronger email validation, structured logging, cancellation handling, configuration-based connection strings, and automated tests.
