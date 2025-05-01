using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    /// <summary>
    /// This class contains the logic for the game.
    /// </summary>
    internal class Logic
    {
        public static readonly string[] Choices = ["rock", "paper", "scissors"];
        /// <summary>
        /// This method is used to get the player's choice.
        /// </summary>
        /// <returns>Player's choice</returns>
        public static string PlayerChoose()
        {
            Console.Write("Choose Rock, Paper or Scissors: ");
            bool HasChosen = false;
            while (HasChosen == false)
            {
                string? Choice = Console.ReadLine();
                if (Choice == null)
                {
                    throw new Exception($"Choice \"{Choice}\" cannot be null");
                }
                Choice = Choice.ToLower(); // Convert to lowercase
                if (Choices.Contains(Choice))
                {
                    HasChosen = true;
                    return Choice;
                }
                else
                {
                    Console.Write("Invalid choice. Please choose Rock, Paper or Scissors: ");
                }
            }
            throw new Exception("Invalid choice. Please choose Rock, Paper or Scissors"); // This line will never be reached because of the while loop
        } // I know it won't ever be reached but otherwise the CS0161 would've stayed and I don't care for that at all right now
          //Why is PlayerChoose() complaining about a CS0161 error when I AM returning the string?

        /// <summary>
        /// This method is used to get the computer's choice.
        /// </summary>
        /// <returns></returns>

        private static readonly Random random = new Random();
        public static string ComputerChoose()
        {
            return Choices[random.Next(0, 3)]; // 0 = Rock, 1 = Paper, 2 = Scissors. [0, 3), 0 included 3 excluded
        }
        /* Original code (mine)
        public static bool HasPlayerWon(string PlayerChoice, string ComputerChoice)
        {
            // Rock > Scissors
            // Paper > Rock
            // Scissors > Paper
            // if PlayerChoice == ComputerChoice then return false; it's a tie
            
            switch (PlayerChoice)
            {
                case "rock": // Rock
                    return ComputerChoice == Choices[2]; // Scissors
                case "paper": // Paper
                    return ComputerChoice == Choices[0]; // Rock
                case "scissors": // Scissors
                    return ComputerChoice == Choices[1]; // Paper
                default:
                    throw new Exception("Invalid choice. Please choose Rock, Paper or Scissors");
            }*/
        // Suggested by ChatGPT
        public static Dictionary<string, string> WinsAgainst = new Dictionary<string, string>
        {
            { Choices[0], Choices[2] }, // Rock > Scissors
            { Choices[1], Choices[0] }, // Paper > Rock
            { Choices[2], Choices[1] }  // Scissors > Paper
        };
        public static bool HasPlayerWon(string PlayerChoice, string ComputerChoice)
        {
            if (PlayerChoice == ComputerChoice) { return false; } // Tie
            return WinsAgainst[PlayerChoice] == ComputerChoice;
        } /* This is just so much easier to read and understand
        Though I do have to ask why I couldn't get the original code to work, I wanted to use Choices[0] instead of "rock" and so on
        Apparently "case" expects constants, values known at compiletime, while arrays are only known at runtime
        Oh well, you win some, you lose some
        Now it's more scalable too so it's not a problem, just have to get good at finding solutions like these by myself*/
        public static string[] PossibleOutcomes = ["You win!", "It's a tie!", "You lose!"];
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
            bool HasChosen = false;
            int Times = 0;
            while (HasChosen == false)
            {
                string? Choice = Console.ReadLine();
                if (int.TryParse(Choice, out Times) && Times > 0)
                {
                    HasChosen = true;
                    return Times;
                }
                else
                {
                    Console.Write("Invalid choice. Please choose a whole number greater than 0: ");
                }
            }
            throw new Exception("Invalid choice. Please choose a whole number greater than 0"); // This line will never be reached because of the while loop
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
            ConsoleColor PreviousBackgroundColor = Console.BackgroundColor;
            ConsoleColor PreviousForegroundColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"You played {Stats.NumberOfGames} games!");
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

            Console.BackgroundColor = PreviousBackgroundColor;
            Console.ForegroundColor = PreviousForegroundColor;
        }
    }
}
