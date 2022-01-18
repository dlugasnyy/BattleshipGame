using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleship;
using Battleship.Models;
using Xunit;

namespace BattleShipUnitTests
{
    public class ShipPositionInitializerTest
    {
        private readonly IShipsPositionsInitializer _shipsPositionsInitializer;
        private Board board;

        public ShipPositionInitializerTest()
        {
            _shipsPositionsInitializer = new ShipsRandomPositionsInitializer();
            board = new Board();
        }
        [Fact]
        public void AfterInitializeNumberOfNotAvailablePositionsShouldBeEqualToSumOfAllShipsLengths()
        {
            var ships = InitializeShips();
            _shipsPositionsInitializer.InitializeShipsPositions(board, ships);
            var shipsLengthsSum = ships.Sum(x => x.Length);
            var numberOfNotAvailablePositions = board.Positions.Count(x => !x.IsAvailable);
            Assert.Equal(shipsLengthsSum, numberOfNotAvailablePositions);
            Assert.All(board.AvailablePositions, pos=>Assert.Null(pos.Symbol) );
        }

        private List<Ship> InitializeShips()
        {
            var ships = new List<Ship>();
            ships.Add(new Ship("Carrier", 5, "C"));
            ships.Add(new Ship("Battleship", 4, "B"));
            ships.Add(new Ship("Cruiser", 3, "c"));
            ships.Add(new Ship("Submarine", 3, "S"));
            ships.Add(new Ship("Destroyer", 2, "D"));
            return ships;
        }
    }
}
