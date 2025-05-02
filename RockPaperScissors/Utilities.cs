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
        public static void ChangeConsoleColors(UserSettings.ConsoleColorSettings colorSettings, bool DoClearConsole = false)
        {
            ConsoleColor bg = Enum.TryParse<ConsoleColor>(colorSettings.BackgroundColor, out var parsedBg) ? parsedBg : Enum.Parse<ConsoleColor>(DefaultColors.NormalBackground);
            ConsoleColor fg = Enum.TryParse<ConsoleColor>(colorSettings.ForegroundColor, out var parsedFg) ? parsedFg : Enum.Parse<ConsoleColor>(DefaultColors.NormalForeground);

            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            if (DoClearConsole) { Console.Clear(); }
        }
    }
}
