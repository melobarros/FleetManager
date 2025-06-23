# FleetManager
 
## Introduction:
Fleet Manager is a simple **.NET web application for managing a fleet of vehicles**. 
It provides **CRUD operations via a REST API**, with full **input validation**, **error handling and unit test coverage**.
###  **NEW!!** 
Added functionality: vehicle diagnostics simulation, with simulated diagnostic protocols, sensors and error codes. You can find a simulation output at the end of the README.


## How to Install and Run
### Cloud
Skip the setup and jump straight into using the application, **the project is deployed into an Azure App Service**.

Link: 
https://fleetmanager-amauri-czesc8beajf4gvfn.brazilsouth-01.azurewebsites.net/swagger

<br>

### Local
Pre Requisites:
- .NET 8 SDK or later. [Download link](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

Clone the repository:
```
git clone https://github.com/melobarros/FleetManager
```

Restore dependencies, build and run:
```
cd FleetManager
dotnet restore
dotnet build
dotnet run --project FleetManager.API
```

Open your browser and go to:
- HTTP: http://localhost:5138/swagger
- HTTPS: https://localhost:7096/swagger

Running tests:
```
dotnet test
```


## About the project
### Features
- **Insert a new vehicle**: supply chassis attributes, type and color; duplicates rejected
- **Edit vehicle color**: Change the Color of a vehicle
- **List all vehicles**: returns full details for every vehicle
- **Find by chassis ID**: returns full details for a single vehicle
- **Delete a vehicle**: removes the record by chassis ID
- **NEW!! - Vehicle Diagnostic**: Simulate a vehicle diagnostic with a specific protocol, sensors and error codes. More details below.

### Vehicle Diagnotic Simulation
- **Research and Definition**: After doing some research on real vehicle diagnostics and protocols like CAN, LIN, FlexRay, J1939 and UDS, I decided to create a simplified simulation, with just Diagnostic Protocol, Sensors and Error Codes.
- A Vehicle now has a Diagnostic Protocol associated, based on the Vehicle Type (Car, Bus, Truck).
- For each protocol, there are a set of sensors, with a Unit of Measure,  Min and Max threshhold for reading values.
- Each sensor has it own Error Code, which is exhibited if the sensor reading is out of the threshholds.
- Each simulation is unique: During the simulation the sensor has a random chance of having a reading out of threshholds.
- All neccessary data is already populated via a Seed.
- You can find a diagnostic simulation output at the end of the README.

### Technologies and libraries
- .NET 8 SDK with a ASP.NET Core WebAPI
- Entity Framework Core with SQLite Database and Migrations. **NEW!!** Seed data for the Vehicle Diagnostic Simulations
- Swagger documentation
- Unit Testing with xUnit and Moq
- Azure and GitHub Actions CI/CD pipeline

### Design Patterns and Best Practices
- **Domain-Driven Design**: focus on modeling the business domain, with entities encapsulating business rules and enforcing invariants in their constructors
- **Factory Pattern**: ensures that the right vehicle class is created, with it's specific number of passengers
- **Repository Pattern**: Repository interface to abstract EF Core implementation
- **Interfaces**: Interfaces to abstract application layers, promoting modularity and facilitating unit testing
- **Inheritance**: the abstract class Vehicle defines the common properties and rules, with each concrete subclass extending it to implement specific rules (such as Number of Passengers based on Vehicle Type)

### Unit Tests
Majority of classes/methods/functions covered by Unit Tests, using xUnit and Moq. 

**NEW!!** - To cover cases where the simulation Seed data is necessary, it is used a Fixture to simulate test context, services and repositories, all of which are initialized and disposed after a test.


## SOLID Principles
Some examples of each principle being applied:

**Single Responsibility Principle**
- Vehicle Class: responsible for it's own rules and behaviours
- VehicleRepository: responsible for data persistence

**Open/Closed**
- Vehicle class is closed for modification, but open for extension by createing a new subclass
- VehicleFactory switch allows to extend new vehicle types without modifying other vehicle types

**Liskov Substitution Principle**
- Car/Truck/Bus classes can replace Vehicle without any issues (polymorphic behaviour)
- Service layer handles VehicleDto, which any Vehicle subtype can use

**Interface Segregation Principle**
- IVehicleRepository exposes only basic CRUD methods, nothing extra
- IVehicleAppService exposes only use cases operations

**Dependency Inversion Principle**
- The AppService depends on the interface of the repository, making the EF Core implementation invisible

## Vehicle Diagnostic Simulation Output
```
{
  "vehicleType": 1,
  "executionDate": "2025-06-23T09:00:25.1388857-03:00",
  "readings": [
    {
      "sensor": "Engine Coolant Temperature",
      "value": 10
    },
    {
      "sensor": "Oil Pressure",
      "value": 76
    },
    {
      "sensor": "Fuel Level",
      "value": 50
    },
    {
      "sensor": "Battery Voltage",
      "value": 12
    },
    {
      "sensor": "Brake Air Pressure",
      "value": 10
    },
    {
      "sensor": "Transmission Oil Temperature",
      "value": 112
    }
  ],
  "errors": [
    {
      "code": "T006",
      "description": "Transmission Oil Overheating"
    }
  ]
}
```

## Credits
Developed by Amauri Barros
<br>
barros.amauri@yahoo.com.br
