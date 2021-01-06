using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Comum
{
    public static class Uteis
    {
        public static bool ModoVerboso { get; set; } = false;
        public static string ImprimeAgora { get; set; } = string.Empty;

        public static string GetFirstDisplayNameEnum<T>(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false
            ) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        public static T GetAttributeFrom<T>(string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = typeof(T).GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).FirstOrDefault();
        }


    }
}
