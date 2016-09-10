using System;
using System.Linq;
using System.Text;

namespace PasswordGenerator
{
    public static class PeselCode
    {
        private static readonly int[] Weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        public static string CreatePesel()
        {
            var result = new StringBuilder(11); 
            var r = new Random();

            //year
            result.Append(r.Next(10)).Append(r.Next(10));

            //month
            result.Append(r.Next(1, 33).ToString("00"));

            //day
            result.Append(r.Next(1, 33).ToString("00"));

            //random numbers
            result.Append(r.Next(10)).Append(r.Next(10)).Append(r.Next(10)).Append(r.Next(10));

            var eq = result.ToString().Select((x, i) => int.Parse(x.ToString()) * Weights[i]).Sum();
            int reszta = eq%10;

            //checksum check
            if (reszta == 0)
            {
                result.Append('0');
            }
            else
            {
                result.Append((10 - reszta).ToString());
            }

            while (!ValidatePesel(result.ToString()))
            {
                //if it somehow fails...
                return CreatePesel();
            }
            
            return result.ToString();
        }

        public static bool ValidatePesel(string pesel)
        {
            var eq = pesel.Substring(0, 10).Select((x, i) => int.Parse(x.ToString()) * Weights[i]).Sum();
            int reszta = eq % 10;

            if (reszta == 0)
            {
                return pesel[10] == '0';
            }
            return 10 - reszta == int.Parse(pesel[10].ToString());
        }
    }
}
