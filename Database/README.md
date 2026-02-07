## Run Application Migrations
Run following code in solution:

```dotnet ef migrations add {Name of migration} --project Database --startup-project ./API --context ApplicationDbContext```

Apply migrations:

```dotnet ef database update --project Database --startup-project ./API --context ApplicationDbContext```

## Run Framework Migrations
Run following code in solution:

```dotnet ef migrations add {Name of migration} --project Framework --startup-project ./API --context FrameworkDbContext```

Apply migrations:

```dotnet ef database update --project Framework --startup-project ./API --context FrameworkDbContext```