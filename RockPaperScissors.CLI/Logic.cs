using RockPaperScissors.Logic;
using static RockPaperScissors.Logic.GameEngine;

namespace RockPaperScissors.CLI
{
    internal class Logic
    {
        public static int GetAmountOfTimes()
        {
            Console.Write("How many times would you like to play? ");
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int times) && times > 0)
                {
                    return times;
                }
                Console.Write("Invalid input. Please enter a number greater than 0: ");
            }
        }

        public static Move GetPlayerMove()
        {
            Console.Write("Choose Rock, Paper, or Scissors: ");
            while (true)
            {
                string? input = Console.ReadLine()?.Trim().ToLower();
                switch (input)
                {
                    case "rock":
                        return Move.Rock;
                    case "paper":
                        return Move.Paper;
                    case "scissors":
                        return Move.Scissors;
                    default:
                        Console.Write("Invalid input. Please choose Rock, Paper, or Scissors: ");
                        break;
                }
            }
        }

        public static GameStats RunInteractiveGameLoop(uint times)
        {
            var stats = new GameStats { NumberOfGames = times };

            foreach (var (player, computer, result) in GameRoundStream(GetPlayerMove, times))
            {
                Console.WriteLine($"\nYou chose: {player}");
                Console.WriteLine($"Computer chose: {computer}");
                DisplayResult(result);

                switch (result)
                {
                    case GameResult.Win:
                        stats.Wins++;
                        break;
                    case GameResult.Loss:
                        stats.Losses++;
                        break;
                    case GameResult.Tie:
                        stats.Ties++;
                        break;
                }
            }

            return stats;
        }

        public static void DisplayResult(GameResult result)
        {
            string message = result switch
            {
                GameResult.Win => "You win!",
                GameResult.Tie => "It's a tie!",
                GameResult.Loss => "You lose!",
                _ => "Unexpected result"
            };
            Console.WriteLine(message);
        }

        public static void ShowStats(GameStats stats)
        {
            Utilities.ChangeConsoleColors(UserSettings.Instance.StatisticsColors);

            Console.WriteLine($"\nYou played {stats.NumberOfGames} games!");
            Console.WriteLine($"Wins: {stats.Wins}");
            Console.WriteLine($"Losses: {stats.Losses}");
            Console.WriteLine($"Ties: {stats.Ties}");

            Console.WriteLine("In conclusion...");
            if (stats.Wins > stats.Losses)
                Console.WriteLine("You are the winner!");
            else if (stats.Wins == stats.Losses)
                Console.WriteLine("It's a draw overall.");
            else
                Console.WriteLine("You are the loser... better luck next time!");

            if (stats.Ties > stats.Wins + stats.Losses)
                Console.WriteLine("You had more ties than wins and losses combined!");

            Utilities.ChangeConsoleColors(UserSettings.Instance.DefaultConsoleColors);
        }
    }
}
