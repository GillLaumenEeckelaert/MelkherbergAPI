using Codes.Shows;

namespace Contracts.Shows.Shared;

public class ShowDto
{
    public Guid ShowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ShowType ShowType { get; set; }
}