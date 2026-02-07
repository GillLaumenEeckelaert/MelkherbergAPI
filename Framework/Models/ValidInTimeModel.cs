namespace Framework.Models;

/// <summary>
/// Models that can be deleted or become inactive over time must inherit this model.
/// </summary>
public class ValidInTimeModel : BaseRootModel
{
    /// <summary>
    /// This object is active starting on this date and time but not before.
    /// Default the value is the time of creation of the object.
    /// </summary>
    public DateTime ValidFrom { get; set; } = DateTime.Now;
    /// <summary>
    /// This object is active until this date and time but not after.
    /// Default the value is max value of time.
    /// </summary>
    public DateTime ValidTo { get; set; } = DateTime.MaxValue;
}