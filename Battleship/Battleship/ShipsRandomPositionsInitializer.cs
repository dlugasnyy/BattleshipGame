using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleship.Models;

namespace Battleship
{
    public class ShipsRandomPositionsInitializer : IShipsPositionsInitializer
    {
        public ShipsRandomPositionsInitializer()
        {

        }

        public void InitializeShipsPositions(Board board, List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                var isShipSet = false;
                while (!isShipSet)
                {
                    var random = new Random();
                    var direction = random.Next(0, 2) % 2;
                    var startRow = random.Next(1, 11);
                    var startColumn = random.Next(1, 11);

                    if (direction == 0)
                    {
                        if (startColumn + ship.Length <= 10)
                        {
                            var horizontalPositions = board.GetHorizontalPositions(startColumn, startColumn + ship.Length, startRow);
                            if (horizontalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = board.Positions.First(pos => pos.X == startColumn + i && pos.Y == startRow);
                                positionForShip.IsAvailable = false;
                                positionForShip.Symbol = ship.Short;
                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var horizontalPositions = board.GetHorizontalPositions(startColumn - ship.Length, startColumn, startRow);
                            if (horizontalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = board.Positions.First(pos => pos.X == startColumn - i && pos.Y == startRow);
                                positionForShip.IsAvailable = false;
                                positionForShip.Symbol = ship.Short;

                            }
                            isShipSet = true;
                        }

                    }
                    else if (direction == 1)
                    {
                        if (startRow + ship.Length <= 10)
                        {
                            var verticalPositions = board.GetVerticalPositions(startRow, startRow + ship.Length, startColumn);
                            if (verticalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = board.Positions.First(pos => pos.Y == startRow + i && pos.X == startColumn);
                                positionForShip.IsAvailable = false;
                                positionForShip.Symbol = ship.Short;

                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var verticalPositions = board.GetVerticalPositions(startRow - ship.Length, startRow, startColumn);
                            if (verticalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = board.Positions.First(pos => pos.Y == startRow - i && pos.X == startColumn);
                                positionForShip.IsAvailable = false;
                                positionForShip.Symbol = ship.Short;

                            }
                            isShipSet = true;
                        }
                    }
                }
            }
        }
    }

    public interface IShipsPositionsInitializer
    {
        void InitializeShipsPositions(Board board, List<Ship> ships);
    }
}
