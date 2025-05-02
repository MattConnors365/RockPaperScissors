using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    /// <summary>
    /// Contains the logic for the game.
    /// </summary>
    internal class Logic
    {
        public static readonly string[] Choices = ["rock", "paper", "scissors"];
        /// <summary>
        /// Used to get the player's choice.
        /// </summary>
        /// <returns>Player's choice</returns>
        public static string PlayerChoose()
        {
            Console.Write("Choose Rock, Paper or Scissors: ");
            while (true)
            {
                string? Choice = Console.ReadLine();
                if (Choice == null) { throw new Exception($"Choice \"{Choice}\" cannot be null"); }
                Choice = Choice.ToLower();
                if (Choices.Contains(Choice)) { return Choice; }
                else { Console.Write("Invalid choice. Please choose Rock, Paper or Scissors: "); }
            }
        }

        private static readonly Random random = new Random();
        /// <summary>
        /// Used to get the computer's choice.
        /// </summary>
        /// <returns></returns>
        public static string ComputerChoose()
        {
            return Choices[random.Next(Choices.Length)]; // 0 = Rock, 1 = Paper, 2 = Scissors.
        }
        /// <summary>
        /// Used to compare the player's choice and the computer's choice.
        /// </summary>
        public static Dictionary<string, string> WinsAgainst = new Dictionary<string, string>
        {
            { Choices[0], Choices[2] }, // Rock > Scissors
            { Choices[1], Choices[0] }, // Paper > Rock
            { Choices[2], Choices[1] }  // Scissors > Paper
        };
        /// <summary>
        /// Used to check if the player has won.
        /// </summary>
        /// <param name="PlayerChoice"></param>
        /// <param name="ComputerChoice"></param>
        /// <returns></returns>
        public static bool HasPlayerWon(string PlayerChoice, string ComputerChoice)
        {
            return WinsAgainst[PlayerChoice] == ComputerChoice;
        } 
        public static string[] PossibleOutcomes = ["You win!", "It's a tie!", "You lose!"];
        /// <summary>
        /// Plays one game of Rock, Paper, Scissors!
        /// </summary>
        /// <returns></returns>
        public static string PlayGame()
        {
            string PlayerChoice = PlayerChoose();
            string ComputerChoice = ComputerChoose();
            Console.WriteLine($"You chose {Utilities.CapitalizeFirstLetter(PlayerChoice)}. Computer chose {Utilities.CapitalizeFirstLetter(ComputerChoice)}");
            if (HasPlayerWon(PlayerChoice, ComputerChoice))
            {
                return PossibleOutcomes[0];
            }
            else if (PlayerChoice == ComputerChoice)
            {
                return PossibleOutcomes[1];
            }
            else
            {
                return PossibleOutcomes[2];
            }
        }
        public static int GetAmountOfTimes()
        {
            Console.Write("How many times would you like to play? ");
            int Times = 0;
            while (true)
            {
                string? Choice = Console.ReadLine();
                if (int.TryParse(Choice, out Times) && Times > 0) { return Times; }
                else { Console.Write("Invalid choice. Please choose a whole number greater than 0: "); }
            }
        }
        public class GameStats
        {
            public uint NumberOfGames = 0;
            public uint Wins = 0;
            public uint Losses = 0;
            public uint Ties = 0;
        };
        public static GameStats PlayMultipleGames(uint Times)
        {
            GameStats Stats = new GameStats();
            Stats.NumberOfGames = Times;
            if (Times < 1)
            {
                throw new Exception("Invalid number of games. Please choose a whole number greater than 0");
            }
            for (int i = 0; i < Times; i++)
            {
                Console.WriteLine($"    Game {i + 1} of {Stats.NumberOfGames}:");
                string result = PlayGame();
                Console.WriteLine(result);
                if (result == PossibleOutcomes[0])
                {
                    Stats.Wins++;
                }
                else if (result == PossibleOutcomes[1])
                {
                    Stats.Ties++;
                }
                else if (result == PossibleOutcomes[2])
                {
                    Stats.Losses++;
                }
                Console.WriteLine("--------------------------------------------------");
            }
            return Stats;
        }
        public static void ShowStats(GameStats Stats)
        {
            Utilities.ChangeConsoleColors(UserSettings.Instance.StatisticsColors);

            Console.WriteLine($"You played a total of {Stats.NumberOfGames} games!");
            Console.WriteLine($"You won {Stats.Wins} times!");
            Console.WriteLine($"You lost {Stats.Losses} times!");
            Console.WriteLine($"You had a tie {Stats.Ties} times!");
            Console.WriteLine("In conclusion...");
            if (Stats.Wins > Stats.Losses)
            {
                Console.WriteLine("You are the winner! You won more games than you lost!");
            }
            else if (Stats.Wins == Stats.Losses)
            {
                Console.WriteLine("You are the winner! You won as many games as you lost!");
            }
            else if (Stats.Wins < Stats.Losses)
            {
                Console.WriteLine("You are the loser! You lost more games than you won!");
            }
            if ((Stats.Ties) > (Stats.Wins + Stats.Losses))
            {
                Console.WriteLine("Also... you had more ties than wins and losses combined!");
            }

            Utilities.ChangeConsoleColors(UserSettings.Instance.DefaultConsoleColors);
        }
    }
}
