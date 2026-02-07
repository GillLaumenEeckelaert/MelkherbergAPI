using Codes.Shows;

namespace Contracts.Shows.Commands;

public class CreateShow
{
    public CreateShowDto Show { get; set; } = new ();
}

public class CreateShowDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ShowType ShowType { get; set; }
}