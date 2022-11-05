using System.ComponentModel;

namespace schedule_appointment_domain.Attributes;

/// <summary>
/// Contains the code to transform an enum in string.
/// </summary>
public class EnumStringValueAttribute : DescriptionAttribute
{
    private readonly string value;

    public EnumStringValueAttribute(string value)
    {
        this.value = value;
    }

    /// <summary>
    /// Returns the string value.
    /// </summary>
    public string Value
    {
        get
        {
            return value;
        }
    }
}