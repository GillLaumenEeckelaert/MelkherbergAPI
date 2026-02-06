## Run Migrations
Run following code in solution:

```dotnet ef migrations add {Name of migration} --project Database --startup-project ./API ```

Apply migrations:

```dotnet ef database update --project Database --startup-project ./API ```