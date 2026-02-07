using System.Reflection;
using Codes.General;
using Contracts.General.Queries;
using Contracts.General.Shared;
using Database;
using Framework.Handlers;

namespace Handlers.General;

public class GetCodeTypesHandler(ApplicationDbContext db) : QueryHandler<GetCodeTypes, GetCodeTypesResponse>
{
    protected override async Task<GetCodeTypesResponse> Execute(GetCodeTypes request)
    {
        List<CodeTypeDto> allCodeTypes = [];
        var enumsAssembly = Assembly.GetAssembly(typeof(Currency));
        Console.WriteLine(enumsAssembly);
        if (enumsAssembly is not null)
        {
            foreach(var enumType in enumsAssembly.GetTypes()
                        .Where(x => x.IsSubclassOf(typeof(Enum))))
            {
                var values = Enum.GetValues(enumType);

                foreach (var value in values)
                {
                    allCodeTypes.Add(new CodeTypeDto
                    {
                        Category = enumType.Name,
                        Name = value.ToString() ?? string.Empty,
                        Value = (int)value
                    });
                }
            }
        }
        
        await Task.CompletedTask;
        
        return new GetCodeTypesResponse
        {
            CodeTypes = allCodeTypes
        };
    }
}