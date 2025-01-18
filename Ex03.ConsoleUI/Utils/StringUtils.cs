using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI.Utils
{
    internal static class StringUtils
    {
        public static string RemovePrefixIfExists(string i_Input, string i_Prefix)
        {
            if (i_Input.StartsWith(i_Prefix))
            {
                return i_Input.Substring(i_Prefix.Length);
            }

            return i_Input;
        }

        public static string SplitCamelCase(string i_Input)
        {
            if (string.IsNullOrEmpty(i_Input))
            {
                return i_Input;
            }

            var result = new StringBuilder();
            result.Append(i_Input[0]);

            for (int i = 1; i < i_Input.Length; i++)
            {
                if (char.IsUpper(i_Input[i]))
                {
                    result.Append(' ');
                }
                result.Append(i_Input[i]);
            }

            return result.ToString();
        }

        public static string EnumOptionsToString(Type i_ParameterType)
        {
            Array enumValues = Enum.GetValues(i_ParameterType);
            StringBuilder sb = new StringBuilder();

            foreach (var enumValue in enumValues)
            {
                sb.Append(enumValue.ToString());
                sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}
