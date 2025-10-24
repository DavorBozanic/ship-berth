SETUP

WEB API

1. After cloning repository, open ShipBerthManagement solution in folder ShipBerthManagementAPI
2. Ensure you have .NET 8.0 SDK installed
3. Visual studio should automatically restore nuget packages, if that doesn't happen run: 'dotnet restore' in CLI
4. Configure the database connection string (if needed add local user & password) for MSSQL Server in ShipBerth.WebAPI/appsettings.json ->
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ShipBerthDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
}
5. Install EF CLI if missing, execute command 'dotnet tool install --global dotnet-ef'
6. Apply EF Core migrations by running these 2 commands:
    Create migration: execute command ->
      'dotnet ef migrations add InitialCreate --project ShipBerth.Infrastructure --startup-project ShipBerth.WebAPI'
   Update database: execute command ->
      'dotnet ef database update --project ShipBerth.Infrastructure --startup-project ShipBerth.WebAPI'
7. Build and run the project, SwaggerUI should open on port https://localhost:7187

CLIENT

1. After cloning repository, open ShipBerthManagementClient folder in Visual Studio Code
2. Install Node.js and npm if needed
3. Install Angular CLI if needed by running command 'npm install -g @angular/cli' in terminal
4. Install dependencies by running command 'npm install' command in terminal
5. If needed edit environment.ts file in src/app/common/configurations/ to match localhost port with WEB API project
6. Start the dev server by running 'ng serve' command
7. Register new user on registration page and login into application with registered credentials
