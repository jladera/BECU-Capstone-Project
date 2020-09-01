using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Locator.Models
{
    /// <summary>
    /// Bool Enum Values
    /// </summary>
    public enum BooleanEnum
    {
        [Display(Name = "No")]
        N = 0,
        [Display(Name = "Yes")]
        Y = 1,
        [Display(Name = "")]
        NULL
    }

    /// <summary>
    /// Extensions for the Bool Enum
    /// </summary>
    public static class BooleanEnumExtensions
    {
        /// <summary>
        /// Call To Title to get the "Yes" or "No"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTitle(this BooleanEnum value)
        {
            var title = value.GetType().GetMember(value.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;

            return title;
        }

        /// <summary>
        /// Call To Value to get the "Y" or "N"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToValue(this BooleanEnum value)
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// Helper for Bool Enum
    /// </summary>
    public class BoolEnumHelper
    {
        /// <summary>
        /// Convert the String to an Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BooleanEnum StringToEnum(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BooleanEnum.NULL;
            }

            var ValueString = value.ToUpper();

            switch (ValueString)
            {
                case "Y":
                case "Yes":
                    return BooleanEnum.Y;

                case "N":
                case "NO":
                    return BooleanEnum.N;

                default:
                    return BooleanEnum.NULL;
            }
        }
    }
}