namespace Framework.Models;

/// <summary>
/// The base model for all models that are considered root models.
/// These are models that are considered entities and need tracking.
/// </summary>
public class BaseRootModel : BaseModel
{
    /// <summary>
    /// Date and time when object was first created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    /// <summary>
    /// Date and time when object was last updated.
    /// An update is any change to one of the fields on the object.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    //Update once authentication is in place
    /// <summary>
    /// Name of user that created this object.
    /// If no user can be connected, the string 'System' will be shown.
    /// </summary>
    public string CreatedBy { get; set; } = "System";
    /// <summary>
    /// Name of user that last updated this object.
    /// If no user can be connected, the string 'System' will be shown.
    /// </summary>
    public string UpdatedBy { get; set; } = "System";
}