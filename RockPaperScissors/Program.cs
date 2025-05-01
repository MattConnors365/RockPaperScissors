using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = UserSettings.Instance; // Loads config and sets colors

            Console.WriteLine("Welcome to Rock, Paper, Scissors!");
            Console.WriteLine("You will play against the computer.");
            int Times = Logic.GetAmountOfTimes();
            Console.WriteLine($"You chose to play {Times} games.");
            Logic.ShowStats(Logic.PlayMultipleGames((uint)Times));
            Console.ReadKey(true);
        }
    }
}
