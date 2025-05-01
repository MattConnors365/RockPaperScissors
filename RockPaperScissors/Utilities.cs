using System;

namespace RockPaperScissors
{
    internal class Utilities
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }
            return (char.ToUpper(input[0]) + input.Substring(1));
        }
        public static void ChangeConsoleColors(ConsoleColor BackGroundColor, ConsoleColor ForegroundColor)
        {
            Console.BackgroundColor = BackGroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.Clear();
        }
    }
}
