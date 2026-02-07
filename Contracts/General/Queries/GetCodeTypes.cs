using Contracts.General.Shared;

namespace Contracts.General.Queries;

public class GetCodeTypes
{
    
} 

public class GetCodeTypesResponse
{
    public List<CodeTypeDto> CodeTypes { get; set; } = [];
}