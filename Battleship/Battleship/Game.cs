using System;
using Battleship.Models;

namespace Battleship
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private int roundCount { get; set; }

        public Game()
        {
            InitializeGame();
            DisplayStartInfo();
        }

        public void PlayGame()
        {
            while (!GameIsFinished())
            {
                PlayRound();
            }
            DisplaySummary();
        }

        private void DisplayStartInfo()
        {
            Console.WriteLine("Player 1 ships:");
            Player1.ShowBoards();
            Console.WriteLine("Player 2 ships:");
            Player2.ShowBoards();
        }

        private void InitializeGame()
        {
            Player1 = new Player("Player 1");
            Player2 = new Player("Player 2");

            Player1.SetShips();
            Player2.SetShips();
        }

        private bool GameIsFinished()
        {
            return Player1.DidLost || Player2.DidLost;
        }

        private void DisplaySummary()
        {
            Player1.ShowBoards();
            Player2.ShowBoards();
            Console.WriteLine($"Player 1 destroyed: {Player2.DestroyedShips} ships");
            Console.WriteLine($"Player 2 destroyed: {Player1.DestroyedShips} ships");
            Console.WriteLine($"Number of rounds: {roundCount}");

            var winnerName = ResolveWinnerName();
            Console.WriteLine($"{winnerName} won the game!");
        }

        private void PlayRound()
        {
            ProcessShooting(Player1, Player2);
            ProcessShooting(Player2, Player1);
            roundCount++;
        }

        private string ResolveWinnerName()
        {
            if (Player1.DidLost)
            {
                return Player2.Name;
            }

            return Player1.Name;
        }

        private void ProcessShooting(Player playerAttacking, Player playerDefending)
        {
            var shotPosition = playerAttacking.Shot();
            LogPlayerShot(playerAttacking, shotPosition);
            Console.WriteLine($"{shotPosition.X}, {shotPosition.Y}" +$"{playerAttacking.Name}");
            var ifHit = playerDefending.CheckPosition(shotPosition);
            if (ifHit)
            {
                var wasShipDestroyed = playerDefending.CommunicateShipDestroy(shotPosition);
                playerAttacking.Process(ifHit, shotPosition, wasShipDestroyed);
            }
            else
            {
                playerAttacking.Process(ifHit, shotPosition);
            }
            // var shotResult = playerDefending.EvaluateShotDamage(shotPosition);
            // playerAttacking.ApplyShotResult(shotPosition, shotResult);

        }

        // TODO Should be introduced class Logger for future replacing loggigng
        private void LogPlayerShot(Player playerAttacking, Position shot)
        {
            // Logger.Log($"{shot.X}, {shot.Y} - {playerAttacking.Name}");
        }
    }
}
