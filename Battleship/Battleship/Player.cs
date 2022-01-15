using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Models;

namespace Battleship
{
    public class Player
    {
        public string Name { get; set; }
        private List<Ship> ships { get; set; }
        public Board Board { get; set; }
        public Board OpponentBoard { get; set; }
        public int DestroyedShips => ships.Count(s => s.isDestroyed());

        private List<Position> hitPositions { get; set; }
        private List<Position> missedPos { get; set; }

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
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    var position = Board.GetPositionFromBoard(row, ownColumn);
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
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    var position = OpponentBoard.GetPositionFromBoard(row, firingColumn);

                    if (position.IsAvailable)
                    {
                        Console.Write("." + "  ");
                    }
                    else
                    {
                        if (position.Symbol == "X")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{position.Symbol}"+ "  ");
                            Console.ResetColor();
                        }
                        else if (position.Symbol == "M")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{position.Symbol}"+ "  ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{position.Symbol}"+ "   ");
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
                            var a = Board.Positions.Where(x =>
                                x.X >= startColumn && x.X <= startColumn + ship.Length && x.Y == startRow).ToList();
                            if (a.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = Board.Positions.First(pos => pos.X == startColumn + i && pos.Y == startRow);
                                x.IsAvailable = false;
                                x.Symbol = ship.Short;
                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var a = Board.Positions.Where(x => x.X > startColumn - ship.Length && x.X <= startColumn && x.Y == startRow).ToList();
                            if (a.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = Board.Positions.First(pos => pos.X == startColumn - i && pos.Y == startRow);

                                x.IsAvailable = false;
                                x.Symbol = ship.Short;

                            }
                            isShipSet = true;
                        }

                    }
                    else if (direction == 1)
                    {
                        if (startRow + ship.Length <= 10)
                        {
                            var a = Board.Positions.Where(x =>
                                x.Y >= startRow && x.Y <= startRow + ship.Length && x.X == startColumn).ToList();
                            if (a.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = Board.Positions.First(pos => pos.Y == startRow + i && pos.X == startColumn);
                                x.IsAvailable = false;
                                x.Symbol = ship.Short;

                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var a = Board.Positions.Where(x => x.Y > startRow - ship.Length && x.Y <= startRow && x.X == startColumn).ToList();
                            if (a.Any(x => !x.IsAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = Board.Positions.First(pos => pos.Y == startRow - i && pos.X == startColumn);
                                x.IsAvailable = false;
                                x.Symbol = ship.Short;

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
                var d = Search();
                if (d != null)
                {
                    return d;
                }
            }
            var random = new Random();
            var freePositions = OpponentBoard.Positions.Where(pos => pos.IsAvailable).ToList();
            var found = false;
            var position = new Position();
            while (!found)
            {
                var randomX = random.Next(1, 11);
                var RandomY = random.Next(1, 11);
                var a = freePositions.FirstOrDefault(pos => pos.Y == RandomY && pos.X == randomX);
                if (a != null)
                {
                    found = true;
                    position = a;
                }
            }
            return position;
        }

        private Position Search()
        {
            var freePositions = OpponentBoard.Positions.Where(pos => pos.IsAvailable).ToList();
            var count = hitPositions.Count;
            if (count == 1)
            {
                var firsttil = hitPositions.First();
                var random = new Random();
                var randomtohit = new List<Position>();
                var positionsinY = freePositions.Where(p =>
                    p.X >= firsttil.X - 1 && p.X <= firsttil.X + 1 && p.Y == firsttil.Y).ToList();
                var positionsinX = freePositions
                    .Where(p => p.Y >= firsttil.Y - 1 && p.Y <= firsttil.Y + 1 && p.X == firsttil.X).ToList();
                randomtohit.AddRange(positionsinX);
                randomtohit.AddRange(positionsinY);
                if (randomtohit.Count != 0)
                {
                    int index = random.Next(randomtohit.Count);
                    return randomtohit[index]; // co jak index zly
                }
                else
                {
                    int index = random.Next(freePositions.Count);
                    return freePositions[index];
                }
            }
            else
            {
                if (hitPositions.First().Y == hitPositions.Last().Y) // sprawdzic M
                {
                    var sorted = hitPositions.OrderBy(a => a.X);
                    if (freePositions.Any(p => p.X == sorted.Last().X + 1 && p.Y == sorted.Last().Y))
                    {
                        return new Position(sorted.Last().Y, sorted.Last().X + 1);
                    }
                    else if (freePositions.Any(p => p.X == sorted.First().X - 1 && p.Y == sorted.First().Y))
                    {
                        return new Position(sorted.First().Y, sorted.First().X - 1);
                    }
                }
                else if (hitPositions.First().X == hitPositions.Last().X)
                {
                    var sortedY = hitPositions.OrderBy(a => a.Y);

                    if (freePositions.Any(p => p.Y == sortedY.Last().Y + 1 && p.X == sortedY.Last().X))
                    {
                        return new Position(sortedY.Last().Y + 1, sortedY.Last().X);
                    }
                    else if (freePositions.Any(p => p.Y == sortedY.First().X - 1 && p.Y == sortedY.First().Y))
                    {
                        return new Position(sortedY.First().Y, sortedY.First().X - 1);
                    }
                }
            }

            return null;
        }

        public bool CheckPosition(Position pos)
        {
            var position = Board.Positions.FirstOrDefault(p => p.Y == pos.Y && p.X == pos.X);
            if (position != null && position.Symbol != null)
            {
                var ship = ships.First(s => s.Short == position.Symbol);
                ship.Hits += 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CommunicateShipDestroy(Position position)
        {
            var myPos = Board.Positions.First(pos => pos.X == position.X && pos.Y == position.Y);

            if (ships.First(s => s.Short == myPos.Symbol).isDestroyed())
                return true;
            else
                return false;
        }

        public void Process(bool isHit, Position position, bool isDestroyed = false)
        {
            var a = OpponentBoard.Positions.First(pos => pos.Y == position.Y && pos.X == position.X);
            a.IsAvailable = false;
            if (isDestroyed)
            {
                a.Symbol = "X";
                hitPositions.Clear();
            }
            else if (isHit)
            {
                a.Symbol = "X";
                hitPositions.Add(position);
            }
            else
            {
                a.Symbol = "M";
            }
        }

    }
}
