using Codes.Shows;

namespace Contracts.Shows.Shared;

public class ShowSummaryDto
{
    public Guid ShowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ShowType ShowType { get; set; }
}