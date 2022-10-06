using System.ComponentModel;

namespace ECommerce.Business.Extensions;

public static class GetEnumDescriptionExtensions
{
    public static string GetEnumDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }
        throw new ArgumentException("Öge bulunamadı.", nameof(enumValue));
    }
}