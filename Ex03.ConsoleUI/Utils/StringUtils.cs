using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI.Utils
{
    internal static class StringUtils
    {
        public static string RemovePrefixIfExists(string input)
        {
            const string k_Prefix = "i_";

            if (input.StartsWith(k_Prefix))
            {
                return input.Substring(k_Prefix.Length);
            }

            return input;
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
    }
}
