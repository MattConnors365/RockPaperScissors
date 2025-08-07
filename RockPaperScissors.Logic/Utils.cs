using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Logic
{
    internal class Utils
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }
            return (char.ToUpper(input[0]) + input.Substring(1));
        }
    }
}
