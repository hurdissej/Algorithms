using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class Convert {

    
        private static string ConvertToBinaryString(double quotient)
        {
            var result = "";
            while(quotient > 0)
            {
                quotient /= 2;
                var remainder = quotient % 1 == 0 ? 0 : 1 ;
                quotient = Math.Floor(quotient);
                result += remainder.ToString();
            }

            return result;
        }

        private static double ConvertFromBinaryString(string binary)
        {
           var rev = binary.Reverse().ToArray();
           double result = 0;
           for(var i =0; i< rev.Length; i++)
           {
               var a = Math.Pow((double)rev[i] * 2, i);
               result += a;
           }
           return result;
        }
}