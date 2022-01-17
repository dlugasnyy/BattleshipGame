using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Models;

namespace Battleship
{
    public interface INextMoveProvider
    {
        Position Shot();
    }
    
    public interface IPlayer
    {
        void ShowBoards();
        void SetShips();
        bool CheckPosition(Position hitPosition);
        bool CommunicateShipDestroy(Position hitPosition);
        void Process(bool isHit, Position hitPosition, bool isDestroyed = false);
    }

    public class Player :IPlayer, INextMoveProvider
    {
        private List<Position> hitPositions { get; set; }
        public string Name { get; set; }
        private List<Ship> ships { get; set; }
        public Board Board { get; set; }
        public Board OpponentBoard { get; set; }
        public int DestroyedShips => ships.Count(s => s.isDestroyed());
        public bool DidLost
        {
            get
            {
                return ships.All(s => s.isDestroyed());
            }
        }

        public Player(string name)
        {
            Initialize();
            Name = name;
            Board = new Board();
            OpponentBoard = new Board();
            hitPositions = new List<Position>();
        }

        private void Initialize()
        {
            ships = new List<Ship>();
            ships.Add(new Ship("Carrier", 5, "C"));
            ships.Add(new Ship("Battleship", 4, "B"));
            ships.Add(new Ship("Cruiser", 3, "c"));
            ships.Add(new Ship("Submarine", 3, "S"));
            ships.Add(new Ship("Destroyer", 2, "D"));
        }

        public void ShowBoards()
        {
            for (int row = 1; row <= 10; row++)
            {
                for (int playerBoardColumn = 1; playerBoardColumn <= 10; playerBoardColumn++)
                {
                    var position = Board.GetPositionFromBoard(row, playerBoardColumn);
                    if (position.IsAvailable)
                    {
                        Console.Write("." + "   ");
                    }
                    else
                    {
                        Console.Write($"{position.Symbol}" + "   ");

                    }

                }
                Console.Write("           ");
                for (int hitBoardColumn = 1; hitBoardColumn <= 10; hitBoardColumn++)
                {
                    var position = OpponentBoard.GetPositionFromBoard(row, hitBoardColumn);

                    if (position.IsAvailable)
                    {
                        Console.Write("." + "  ");
                    }
                    else
                    {
                        if (position.Symbol == "X")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{position.Symbol}" + "  ");
                            Console.ResetColor();
                        }
                        else if (position.Symbol == "M")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{position.Symbol}" + "  ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{position.Symbol}" + "   ");
                        }
                    }
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void SetShips()
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
                            var horizontalPositions = Board.GetHorizontalPositions(startColumn, startColumn + ship.Length, startRow);
                            if (horizontalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = Board.Positions.First(pos => pos.X == startColumn + i && pos.Y == startRow);
                                positionForShip.IsAvailable = false;
                                positionForShip.Symbol = ship.Short;
                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var horizontalPositions = Board.GetHorizontalPositions(startColumn - ship.Length, startColumn, startRow);
                            if (horizontalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = Board.Positions.First(pos => pos.X == startColumn - i && pos.Y == startRow);
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
                            var verticalPositions = Board.GetVerticalPositions(startRow, startRow + ship.Length, startColumn);
                            if (verticalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = Board.Positions.First(pos => pos.Y == startRow + i && pos.X == startColumn);
                                positionForShip.IsAvailable = false;
                                positionForShip.Symbol = ship.Short;

                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var verticalPositions = Board.GetVerticalPositions(startRow - ship.Length, startRow, startColumn);
                            if (verticalPositions.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var positionForShip = Board.Positions.First(pos => pos.Y == startRow - i && pos.X == startColumn);
                                positionForShip.IsAvailable = false;
                                positionForShip.Symbol = ship.Short;

                            }
                            isShipSet = true;
                        }
                    }
                }
            }
        }

        public Position Shot()
        {
            if (hitPositions.Count > 0)
            {
                Console.WriteLine($"{Name} search shot");
                var foundPosition = SearchShot();
                if (foundPosition != null)
                {
                    return foundPosition;
                }
            }
            Console.WriteLine($"{Name} random shot");

            return RandomShot();
        }

        private Position RandomShot()
        {
            var random = new Random();
            var availablePositions = OpponentBoard.AvailablePositions;
            var found = false;
            var position = new Position();
            while (!found)
            {
                var randomColumn = random.Next(1, 11);
                var randomRow = random.Next(1, 11);
                var foundPosition = availablePositions.FirstOrDefault(pos => pos.Y == randomRow && pos.X == randomColumn);
                if (foundPosition != null)
                {
                    found = true;
                    position = foundPosition;
                }
            }
            return position;
        }

        private Position SearchShot()
        {
            var availablePositions = OpponentBoard.AvailablePositions;
            Console.WriteLine($"available positions: {availablePositions.Count}");
            var hitPositionsCount = hitPositions.Count;
            if (hitPositionsCount == 1)
            {
                var firstPosition = hitPositions.First();
                var random = new Random();
                var closePositionsToHit = new List<Position>();
                var positionsInRow = availablePositions.Where(p =>
                    p.X >= firstPosition.X - 1 && p.X <= firstPosition.X + 1 && p.Y == firstPosition.Y).ToList();
                var positionsInColumn = availablePositions
                    .Where(p => p.Y >= firstPosition.Y - 1 && p.Y <= firstPosition.Y + 1 && p.X == firstPosition.X).ToList();
                closePositionsToHit.AddRange(positionsInColumn);
                closePositionsToHit.AddRange(positionsInRow);
                if (closePositionsToHit.Count != 0)
                {
                    var index = random.Next(closePositionsToHit.Count);
                    return closePositionsToHit[index];
                }
                else
                {
                    //in case if alghoritm made mistake because of two ships next to each other
                    hitPositions.Clear();
                    var index = random.Next(availablePositions.Count);
                    return availablePositions[index];
                }
            }
            else
            {
                if (hitPositions.First().Y == hitPositions.Last().Y)
                {
                    var sortedHitPositionsByRow = hitPositions.OrderBy(a => a.X);
                    if (availablePositions.Any(p => p.X == sortedHitPositionsByRow.Last().X + 1 && p.Y == sortedHitPositionsByRow.Last().Y))
                    {
                        return new Position(sortedHitPositionsByRow.Last().Y, sortedHitPositionsByRow.Last().X + 1);
                    }
                    else if (availablePositions.Any(p => p.X == sortedHitPositionsByRow.First().X - 1 && p.Y == sortedHitPositionsByRow.First().Y))
                    {
                        return new Position(sortedHitPositionsByRow.First().Y, sortedHitPositionsByRow.First().X - 1);
                    }
                }
                else if (hitPositions.First().X == hitPositions.Last().X)
                {
                    var sortedHitPositionsByColumn = hitPositions.OrderBy(a => a.Y);

                    if (availablePositions.Any(p => p.Y == sortedHitPositionsByColumn.Last().Y + 1 && p.X == sortedHitPositionsByColumn.Last().X))
                    {
                        return new Position(sortedHitPositionsByColumn.Last().Y + 1, sortedHitPositionsByColumn.Last().X);
                    }
                    else if (availablePositions.Any(p => p.Y == sortedHitPositionsByColumn.First().Y - 1 && p.X == sortedHitPositionsByColumn.First().X))
                    {
                        return new Position(sortedHitPositionsByColumn.First().Y - 1, sortedHitPositionsByColumn.First().X);
                    }
                }
            }
            return null;
        }

        public bool CheckPosition(Position hitPosition)
        {
            var position = Board.GetPositionFromBoard(hitPosition.Y, hitPosition.X);
            if (position != null && position.Symbol != null)
            {
                var ship = ships.First(s => s.Short == position.Symbol);
                ship.Hits += 1;
                return true;
            }
            return false;
        }

        public bool CommunicateShipDestroy(Position hitPosition)
        {
            var position = Board.GetPositionFromBoard(hitPosition.Y, hitPosition.X);
            if (ships.First(s => s.Short == position.Symbol).isDestroyed())
                return true;
            else
                return false;
        }

        public void Process(bool isHit, Position hitPosition, bool isDestroyed = false)
        {
            var position = OpponentBoard.GetPositionFromBoard(hitPosition.Y, hitPosition.X);
            position.IsAvailable = false;
            if (isDestroyed)
            {
                position.Symbol = "X";
                hitPositions.Clear();
            }
            else if (isHit)
            {
                position.Symbol = "X";
                hitPositions.Add(hitPosition);
            }
            else
            {
                position.Symbol = "M";
            }
        }

    }
}
