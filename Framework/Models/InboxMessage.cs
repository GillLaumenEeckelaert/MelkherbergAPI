using Framework.Models;

namespace Models.Logs;

public class InboxMessage : BaseModel
{
    public Guid InboxMessageId { get; set; }

    public DateTime Received { get; set; } = DateTime.Now;
    public DateTime? Handled { get; set; }

    public string Type { get; set; } = null!;
    public string Content { get; set; } = null!;
    
    public bool Success { get; set; }
    public string? Status { get; set; }
    public string? Error { get; set; }
    
    public string? UserName { get; set; }
    public Guid? UserId { get; set; }
    public string? SourceType { get; set; }
    public string? Source { get; set; }
}