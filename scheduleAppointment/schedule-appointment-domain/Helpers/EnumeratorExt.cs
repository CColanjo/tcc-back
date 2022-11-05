using schedule_appointment_domain.Attributes;

namespace System;

public static class EnumeratorExt
{
    /// <summary>
    /// Returns the string value of enum value.
    /// </summary>
    /// <param name="usesToString">Allow return the value string (ToString) whether the enum was not configured with a value string.</param>
    /// <returns>Returns null whether the enum was not configured with a value string and the parameter usesToString is false.</returns>
    public static string GetValueString(this Enum value, bool usesToString = false)
    {
        var valueInfo = value.GetType().GetField(value.ToString());

        var stringValueAttributes = (EnumStringValueAttribute[])valueInfo.GetCustomAttributes(typeof(EnumStringValueAttribute), true);

        if (stringValueAttributes.Length > 0)
            return stringValueAttributes[0].Value;

        if (usesToString)
            return value.ToString();

        return null;
    }
}