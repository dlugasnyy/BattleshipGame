using System;
using System.Collections.Generic;
using Battleship.Models;
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
            // var expectedPosition = new List<Position>() {new Position()};
            // var result = board.GetPositionFromBoard(3, 8);
            // Assert.Equal(expectedPosition.Y, result.Y);
            // Assert.Equal(expectedPosition.X, result.X);
        }

        [Fact]
        public void GetVerticalPositionFromBoardShouldReturnExpectedPosition()
        {
            var expectedPosition = new Position() { X = 8, Y = 3 };
            var result = board.GetPositionFromBoard(3, 8);
            Assert.Equal(expectedPosition.Y, result.Y);
            Assert.Equal(expectedPosition.X, result.X);
        }

        [Fact]
        public void GetHorizontalPositionFromBoardShouldReturnExpectedPosition()
        {
            var expectedPosition = new Position() { X = 8, Y = 3 };
            var result = board.GetPositionFromBoard(3, 8);
            Assert.Equal(expectedPosition.Y, result.Y);
            Assert.Equal(expectedPosition.X, result.X);
        }
    }
}
