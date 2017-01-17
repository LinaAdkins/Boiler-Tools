using System;

namespace BoilerTools.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Change the first letter of the string to upper-case.
        /// </summary>
        public static string FirstLetterToUpper(this string str)
        {
            if( str == null) { return null; }

            if(str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }

            return str.ToUpper();
        }
    }
}
