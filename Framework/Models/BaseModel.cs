namespace Framework.Models;

/// <summary>
/// Simple models that do not fit any other type of model, nust inherit this model.
/// </summary>
public class BaseModel
{
    /// <summary>
    /// GUID that identifies this object.
    /// </summary>
    public Guid ObjectId { get; set; } = Guid.NewGuid();
}