# MakeMeRich - API

A web API for managing personal finances. The API was created by follwing REST standard and 
the Clean Architecture principles.

The API was created mainly to satisfy personal needs as i want to create more personalised app than Microsoft Money 
which i am currently using but also i wanted to learn how to use patterns like CQRS and Mediator.

**Check current API definition** -> [SwaggerDoc](https://sebastianstudniczek.github.io/MakeMeRich/)

## Table of Contents
* [Technologies](#Technologies)
* [Getting Started](#Getting-Started)
* [Project Status](#Project-Status)
* [Sources](#Sources)
 
## Technologies

* .NET Core 5.0 (C# 9)
* ASP .NET Core 5.0
* Entity Framework Core 5
* MediatR 9
* AutoMapper 10
* Fluent Validation 9
* Serilog 3 
* Swagger 5
* xUnit 2
* Fluent Assertions 5

## Getting Started

1. Install the latest [.NET Core SDK](https://dotnet.microsoft.com/download)
2. Get the repository with `git clone https://github.com/sebastianstudniczek/MakeMeRich`.
3. Navigate to `Source/WebAPI` and run `dotnet run` to launch the back end with Swagger document.

### Database Configuration

For ensuring that all users will be able to run the solution without needing to set up an additional infrastructure (e.g. SQL Server), 
the project is configured to use an in-memory database by default.

To change this, you will need to update **WebAPI/appsettings.json** as follows:

```json
    "UseInMemoryDatabase": false,
```
Verify that the **Default Connection** connection string within **appsettings.json** points to a valid SQL Server instance.

## Project Status

#### Implemented features

* Storing and managing:
  * Financial accounts
  * Financial categories
  * Expenses and Incomes
* Basic authentication and authorization using JWT

#### To Do

* Budget:
  * Create budget
  * Track current expenses
* Complex identity (using IdentityServer)
* Import financial transactions from .xlsx file

## Sources

The project architecture is mainly based on `Clean Architecture Solution Template`
by [@jasontaylordev](#https://github.com/jasontaylordev/CleanArchitecture).



