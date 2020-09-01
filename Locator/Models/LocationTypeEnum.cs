using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Locator.Models
{
    public enum LocationTypeEnum
    {
        [Display(Name = "ATM")]
        ATM,
        [Display(Name = "ATM")]
        A,
        [Display(Name = "Shared Branch")]
        S,
        [Display(Name = "Unknown")]
        Unknown
    }

    public static class LocationTypeEnumExtensions
    {
        public static string ToTitle(this LocationTypeEnum value)
        {
            var title = value.GetType().GetMember(value.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;

            return title;
        }
    }

    public class LocationTypeEnumHelper
    {
        public static LocationTypeEnum StringToEnum(string value)
        {
            try
            {
                return (LocationTypeEnum)Enum.Parse(typeof(LocationTypeEnum), value);
            }
            catch (Exception)
            {
                return LocationTypeEnum.Unknown;
            }
        }
    }
}