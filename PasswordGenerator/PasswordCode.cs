using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PasswordGenerator
{
    public static class PasswordCode
    {
        internal static IEnumerable<char> GetSymbols(CheckBox checkBox, TextBox allowedSymbolsBox)
        {
            if (checkBox.IsChecked == true)
            {
                return allowedSymbolsBox.Text.Trim().Replace(" ", "");
            }
            return Enumerable.Empty<char>();
        }

        public static string CreatePassword(int length, bool capitals, bool digits, char[] symbols)
        {
            var allowedInts = new List<int>(Enumerable.Range(97, 26));
            if (capitals)
            {
                allowedInts.AddRange(Enumerable.Range(65, 26));
            }
            if (digits)
            {
                allowedInts.AddRange(Enumerable.Range(48, 10));
            }

            var chars = new List<char>();
            if (symbols.Any())
            {
                chars.AddRange(symbols);
            }
            chars.AddRange(allowedInts.Select(x => (char)x));

            var r = new Random();
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                sb.Append(chars[r.Next(chars.Count)]);
            }

            var result = sb.ToString();
            if (!result.Any(char.IsLower))
            {
                result = result.Remove(result.Length / 4, 1);
                result = result.Insert(result.Length, chars.FirstOrDefault(char.IsLower).ToString());
            }
            if (capitals && !result.Any(char.IsUpper))
            {
                result = result.Remove(result.Length / 3, 1);
                result = result.Insert(result.Length, chars.FirstOrDefault(char.IsUpper).ToString());
            }
            if (digits && !result.Any(char.IsDigit))
            {
                result = result.Remove(result.Length / 2, 1);
                result = result.Insert(result.Length, chars.FirstOrDefault(char.IsDigit).ToString());
            }
            if (capitals && !result.Any(char.IsSymbol))
            {
                result = result.Remove(result.Length - 1, 1);
                result = result.Insert(result.Length, chars.FirstOrDefault(char.IsSymbol).ToString());
            }
            return result;
        }

        internal static int GetLength(TextBox lengthBox)
        {
            double result;
            if (double.TryParse(lengthBox.Text, out result))
            {
                return (int)Math.Abs(Math.Floor(result));
            }
            throw new ArgumentException("Could not parse password length.");
        }

        internal static bool GetCheckBoxValue(CheckBox checkBox)
        {
            return checkBox.IsChecked ?? false;
        }
    }
}
