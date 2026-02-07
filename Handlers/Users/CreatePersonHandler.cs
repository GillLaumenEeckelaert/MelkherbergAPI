using Contracts.Shows.Commands;
using Contracts.Users.Commands;
using Database;
using Framework.Handlers;
using Models.General;

namespace Handlers.Users;

public class CreatePersonHandler(ApplicationDbContext db) : CommandHandler<CreatePerson>
{
    protected override async Task Execute(CreatePerson request)
    {
        var existingPerson = db.Person.FirstOrDefault(s => string.Equals(s.FirstName, request.Person.FirstName) && string.Equals(s.LastName, request.Person.LastName));

        if (existingPerson != null)
        {
            throw new Exception($"Person with name {request.Person.FirstName} {request.Person.LastName} already exists");
        }

        await db.Person.AddAsync(new Person
        {
            FirstName = request.Person.FirstName ?? string.Empty,
            LastName = request.Person.LastName,
            NickName = request.Person.NickName,
            Birthday = request.Person.BirthDate is not null ? new DateOnly(request.Person.BirthDate.Value.Year, request.Person.BirthDate.Value.Month,request.Person.BirthDate.Value.Day) : null,
            IsSelectable =  request.Person.IsSelectable,
            UserId = request.Person.UserId
        });
        await db.SaveChangesAsync();
    }
}