using Battleship;
using Xunit;

namespace BattleShipUnitTests
{
    public class PlayerTest
    {
        private readonly IShipsPositionsInitializer _shipsPositionsInitializer;
        private readonly IPlayerShotProvider _playerShotProvider;
        private const string Name = "Player";
        
        public PlayerTest()
        {
            _shipsPositionsInitializer = new ShipsRandomPositionsInitializer();
            _playerShotProvider = new PlayerShotProvider();
        }

        [Fact]
        public void CheckPlayerAfterCreating()
        {
            var expectedDestroyedShips = 0;
            var player = new Player(Name, _playerShotProvider, _shipsPositionsInitializer);
            var numberOfBoardPositions = player.Boards.Board.AvailablePositions.Count;
            var numberOfOpponentBoardPositions = player.Boards.OpponentBoard.AvailablePositions.Count;
            var expectedNumber = 100;
            Assert.Equal(expectedNumber, numberOfBoardPositions);
            Assert.Equal(expectedNumber, numberOfOpponentBoardPositions);
            Assert.Equal(expectedDestroyedShips, player.DestroyedShips);
        }
    }
}
