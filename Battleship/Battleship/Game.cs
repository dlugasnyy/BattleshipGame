using System;

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
        }

        private void InitializeGame()
        {
            Player1 = new Player("Player 1");
            Player2 = new Player("Player 2");
            Player1.SetShips();
            Player2.SetShips();
            Console.WriteLine("Player 1 sips:");
            Player1.ShowBoards();
            Console.WriteLine("Player 2 sips:");
            Player2.ShowBoards();
        }

        public void PlayGame()
        {
            while (!GameIsFinished())
            {
                PlayRound();
            }
            DisplaySummary();

            if (Player1.DidLost)
            {
                Console.WriteLine("Player 2 won the game!");
            }

            if (Player2.DidLost)
            {
                Console.WriteLine("Player 1 won the game!");
            }

        }

        private bool GameIsFinished()
        {
            if (Player1.DidLost || Player2.DidLost)
            {
                return true;
            }
            return false;
        }

        private void DisplaySummary()
        {
            Player1.ShowBoards();
            Player2.ShowBoards();
            Console.WriteLine($"Player 1 destroyed: {Player2.DestroyedShips} ships");
            Console.WriteLine($"Player 2 destroyed: {Player1.DestroyedShips} ships");
            Console.WriteLine($"Number of rounds: {roundCount}");
        }

        private void PlayRound()
        {
            var player1Shot = Player1.Shot();
            Console.WriteLine($"{player1Shot.X}, {player1Shot.Y}" + " player 1");
            var player1DidHit = Player2.CheckPosition(player1Shot);
            if (player1DidHit)
            {
                var wasShipDestroyed = Player2.CommunicateShipDestroy(player1Shot);
                Player1.Process(player1DidHit, player1Shot, wasShipDestroyed);
            }
            else
            {
                Player1.Process(player1DidHit, player1Shot);
            }

            if (!Player2.DidLost)
            {
                var player2Shot = Player2.Shot();
                Console.WriteLine($"{player2Shot.X}, {player2Shot.Y}" + " player 2");
                var player2DidHit = Player1.CheckPosition(player2Shot);
                if (player2DidHit)
                {
                    var wasShipDestroyed = Player1.CommunicateShipDestroy(player2Shot);
                    Player2.Process(player2DidHit, player2Shot, wasShipDestroyed);
                }
                else
                {
                    Player2.Process(player2DidHit, player2Shot);
                }
            }
            roundCount++;
        }
    }
}
