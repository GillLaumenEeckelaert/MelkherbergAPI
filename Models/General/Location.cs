using Framework.Models;

namespace Models.Test;

public class TestFile : BaseModel
{
    public Guid TestFileId { get; set; }
    public required string Name { get; set; }
    public required string FilePath { get; set; }
    public required string Extension { get; set; }
    public int Size { get; set; }
}