using System;
using System.Collections.Generic;

namespace RockPaperScissors.Logic
{
    public static class GameEngine
    {
        public enum Move
        {
            Rock,
            Paper,
            Scissors
        }
        public enum GameResult
        {
            Win,
            Tie,
            Loss
        }

        public class GameStats
        {
            public uint NumberOfGames { get; set; }
            public uint Wins { get; set; }
            public uint Losses { get; set; }
            public uint Ties { get; set; }
        }

        public static readonly Move[] Choices = new[] { Move.Rock, Move.Paper, Move.Scissors };

        private static readonly Dictionary<Move, Move> WinsAgainst = new()
        {
            { Move.Rock, Move.Scissors },
            { Move.Paper, Move.Rock },
            { Move.Scissors, Move.Paper }
        };

        private static readonly Random random = new();

        public static Move ComputerChoose()
        {
            return Choices[random.Next(Choices.Length)];
        }

        public static bool HasPlayerWon(Move player, Move computer)
        {
            return WinsAgainst[player] == computer;
        }

        public static string ToDisplayString(Move move)
        {
            return move.ToString();
        }

        /// <summary>
        /// Core game logic: you pass in both choices, it returns the result string.
        /// </summary>
        public static GameResult PlayGame(Move playerChoice, Move computerChoice)
        {
            if (HasPlayerWon(playerChoice, computerChoice))
                return GameResult.Win;
            else if (playerChoice == computerChoice)
                return GameResult.Tie;
            else
                return GameResult.Loss;
        }
        public static IEnumerable<(Move Player, Move Computer, GameResult Result)> GameRoundStream
            (Func<Move> playerChoiceProvider, uint times)
        {
            if (times < 1)
                throw new ArgumentException("Number of games must be greater than 0", nameof(times));

            for (int i = 0; i < times; i++)
            {
                Move player = playerChoiceProvider();
                Move computer = ComputerChoose();
                GameResult result = PlayGame(player, computer);
                yield return (player, computer, result);
            }
        }

        /// <summary>
        /// Used by GUI/CLI/etc. to simulate N games and track stats.
        /// The caller is responsible for gathering user input.
        /// </summary>
        public static GameStats PlayMultipleGames(Func<Move> playerChoiceProvider, uint times)
        {
            var stats = new GameStats { NumberOfGames = times };

            foreach (var (_, _, result) in GameRoundStream(playerChoiceProvider, times))
            {
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
    }
}
