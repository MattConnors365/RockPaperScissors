using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RockPaperScissors
{
    public class UserSettings
    {
        // Define defaults in one place only
        private const string DefaultBackgroundColor = "Black";
        private const string DefaultForegroundColor = "Green";

        [JsonPropertyName("Background_Color")]
        public string BackgroundColor { get; set; } = DefaultBackgroundColor;
        [JsonPropertyName("Text_Color")]
        public string ForegroundColor { get; set; } = DefaultForegroundColor;

        private static UserSettings? _instance;

        public static UserSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    string path = "./Settings.json";
                    if (!File.Exists(path))
                    {
                        _instance = new UserSettings();
                        File.WriteAllText(path, JsonSerializer.Serialize(_instance, new JsonSerializerOptions { WriteIndented = true }));
                    }
                    else
                    {
                        string json = File.ReadAllText(path);
                        _instance = JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
                    }

                    _instance.ApplyConsoleColorsFromSettings();
                }

                return _instance;
            }
        }

        private void ApplyConsoleColorsFromSettings()
        {
            ConsoleColor bg = Enum.TryParse(
                BackgroundColor, out ConsoleColor parsedBg) ? parsedBg : Enum.Parse<ConsoleColor>(DefaultBackgroundColor);
            ConsoleColor fg = Enum.TryParse(
                ForegroundColor, out ConsoleColor parsedFg) ? parsedFg : Enum.Parse<ConsoleColor>(DefaultForegroundColor);

            Utilities.ChangeConsoleColors(bg, fg);
        }
    }
}
