using Contracts.Shows.Commands;
using Database;
using Framework.Handlers;
using Models.Shows;

namespace Handlers.Shows;

public class CreateShowHandler(ApplicationDbContext db) : CommandHandler<CreateShow>
{
    protected override async Task Execute(CreateShow request)
    {
        var existingShow = db.Show.FirstOrDefault(s => string.Equals(s.Name, request.Show.Name));

        if (existingShow != null)
        {
            throw new Exception($"Show with name {request.Show.Name} already exists");
        }

        await db.Show.AddAsync(new Show
        {
            ShowId = Guid.NewGuid(),
            Name = request.Show.Name,
            Description = request.Show.Description,
            ShowType = request.Show.ShowType,
        });
        await db.SaveChangesAsync();
    }
}