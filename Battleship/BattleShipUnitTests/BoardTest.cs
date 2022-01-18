using System.Collections.Generic;
using Battleship.Models;
using Newtonsoft.Json;
using Xunit;
using Board = Battleship.Models.Board;

namespace BattleShipUnitTests
{
    public class BoardTest
    {
        private Board board;

        public BoardTest()
        {
            board = new Board();
        }

        [Fact]
        public void NumberOfPositionsShouldBe100()
        {
            var numberOfPositions = board.Positions.Count;
            var expectedNumber = 100;
            Assert.Equal(expectedNumber, numberOfPositions);
        }

        [Fact]
        public void GetPositionFromBoardShouldReturnExpectedPosition()
        {
            var expectedPosition = new Position() { X = 8, Y = 3 };
            var result = board.GetPositionFromBoard(expectedPosition);
            Assert.Equal(expectedPosition.Y, result.Y);
            Assert.Equal(expectedPosition.X, result.X);
        }

        [Fact]
        public void GetPositionFromBoardShouldReturnNullIfThereIsNoPosition()
        {
            var expectedPosition = new Position() { X = 80, Y = 3 };
            var result = board.GetPositionFromBoard(expectedPosition);
            Assert.Null(result);
        }

        [Fact]
        public void GetPositionByCoordsShouldReturnExpectedPosition()
        {
            var expectedPosition = new Position() { X = 8, Y = 3 };
            var result = board.GetPositionByCoords(expectedPosition.Y, expectedPosition.X);
            Assert.Equal(expectedPosition.Y, result.Y);
            Assert.Equal(expectedPosition.X, result.X);
        }

        [Fact]
        public void GetPositionByCoordsShouldReturnNullIfThereIsNoCoords()
        {
            var expectedPosition = new Position() { X = 80, Y = 3 };
            var result = board.GetPositionByCoords(expectedPosition.Y, expectedPosition.X);
            Assert.Null(result);
        }

        [Fact]
        public void GetVerticalPositionFromBoardShouldReturnExpectedPosition()
        {
            var expectedPositions = new List<Position>() { new Position() { X = 3, Y = 1, IsAvailable = true }, new Position() { X = 3, Y = 2, IsAvailable = true } };
            var result = board.GetVerticalPositions(1, 2, 3);
            Assert.Equal(JsonConvert.SerializeObject(expectedPositions), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void GetHorizontalPositionFromBoardShouldReturnExpectedPosition()
        {
            var expectedPositions = new List<Position>() { new Position() { X = 2, Y = 2, IsAvailable = true }, new Position() { X = 3, Y = 2, IsAvailable = true } };
            var result = board.GetHorizontalPositions(2, 3, 2);
            Assert.Equal(JsonConvert.SerializeObject(expectedPositions), JsonConvert.SerializeObject(result));
        }
    }
}
