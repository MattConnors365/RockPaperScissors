using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RockPaperScissors
{
    public class UserSettings
    {
        public class ConsoleColorSettings
        {
            [JsonPropertyName("Background_Color")]
            public string BackgroundColor { get; set; }

            [JsonPropertyName("Text_Color")]
            public string ForegroundColor { get; set; }
            public ConsoleColorSettings(
                string backgroundColor = DefaultColors.NormalBackground, string foregroundColor = DefaultColors.NormalForeground)
            {
                BackgroundColor = backgroundColor;
                ForegroundColor = foregroundColor;
            }
        }

        public ConsoleColorSettings DefaultConsoleColors { get; set; } = 
            new(DefaultColors.NormalBackground, DefaultColors.NormalForeground);
        public ConsoleColorSettings StatisticsColors { get; set; } = 
            new(DefaultColors.StatisticsBackground, DefaultColors.StatisticsForeground);

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

                    Utilities.ChangeConsoleColors(_instance.DefaultConsoleColors, true);
                }

                return _instance;
            }
        }
    }
    public static class DefaultColors
    {
        public const string NormalBackground = "Black";
        public const string NormalForeground = "Green";
        public const string StatisticsBackground = "Black";
        public const string StatisticsForeground = "Red";
    }
}
