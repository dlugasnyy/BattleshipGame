using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleship.Models;
using Microsoft.VisualBasic;

namespace Battleship
{
    public class Player
    {
        public string Name { get; set; }
        private List<Ship> ships { get; set; }
        public Board board { get; set; }
        public Board oponentBoard { get; set; }
        private int destroyedShips { get; set; }
        private List<Position> hitpos { get; set; }
        private List<Position> missedPos { get; set; }

        public Player(string name)
        {
            Initialize();
            Name = name;
            board = new Board();
            oponentBoard = new Board();
            hitpos = new List<Position>();
            destroyedShips = 0;
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
                    var pan = board.GetPositionFromBoard(row, ownColumn);
                    if (pan.isAvailable)
                    {
                        Console.Write("." + "   ");
                    }
                    else
                    {
                        Console.Write($"{pan.symbol}" + "   ");

                    }

                }
                Console.Write("                ");
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    var pan = oponentBoard.GetPositionFromBoard(row, firingColumn);

                    if (pan.isAvailable)
                    {
                        Console.Write("." + "   ");
                    }
                    else
                    {
                        // Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write($"{pan.symbol}" + "   ");
                    }
                    // Console.ResetColor();
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
                            var a = board.Positions.Where(x =>
                                x.X >= startColumn && x.X <= startColumn + ship.Length && x.Y == startRow).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.X == startColumn + i && pos.Y == startRow);
                                x.isAvailable = false;
                                x.symbol = ship.Short;
                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var a = board.Positions.Where(x => x.X > startColumn - ship.Length && x.X <= startColumn && x.Y == startRow).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.X == startColumn - i && pos.Y == startRow);

                                x.isAvailable = false;
                                x.symbol = ship.Short;

                            }
                            isShipSet = true;
                        }

                    }
                    else if (direction == 1)
                    {
                        if (startRow + ship.Length <= 10)
                        {
                            var a = board.Positions.Where(x =>
                                x.Y >= startRow && x.Y <= startRow + ship.Length && x.X == startColumn).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.Y == startRow + i && pos.X == startColumn);
                                x.isAvailable = false;
                                x.symbol = ship.Short;

                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var a = board.Positions.Where(x => x.Y > startRow - ship.Length && x.Y <= startRow && x.X == startColumn).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.Y == startRow - i && pos.X == startColumn);
                                x.isAvailable = false;
                                x.symbol = ship.Short;

                            }
                            isShipSet = true;

                        }

                    }
                }

            }
        }

        public Position Shoot()
        {
            if (hitpos.Count > 0)
            {
                var d = search();
                if (d != null)
                {
                    return d;
                }
            }
            var random = new Random();
            var freePositions = oponentBoard.Positions.Where(pos => pos.isAvailable).ToList();
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

        public Position search()
        {
            var freePositions = oponentBoard.Positions.Where(pos => pos.isAvailable).ToList();
            var count = hitpos.Count;
            if (count == 1)
            {
                var firsttil = hitpos.First();
                var random = new Random();
                var randomtohit = new List<Position>();
                var positionsinY = freePositions.Where(p =>
                    p.X >= firsttil.X - 1 && p.X <= firsttil.X + 1 && p.Y == firsttil.Y).ToList();
                var positionsinX = freePositions.Where(p=> p.Y >= firsttil.Y - 1 && p.Y <= firsttil.Y + 1 && p.X == firsttil.X).ToList();
                randomtohit.AddRange(positionsinX);
                randomtohit.AddRange(positionsinY);
                int index = random.Next(randomtohit.Count);
                return randomtohit[index]; // co jak index zly
            }
            else
            {
                if (hitpos.First().Y == hitpos.Last().Y) // sprawdzic M
                {
                    var sorted = hitpos.OrderBy(a=>a.X);
                    if (freePositions.Any(p => p.X == sorted.Last().X + 1 && p.Y == sorted.Last().Y))
                    {
                        return new Position(sorted.Last().Y, sorted.Last().X + 1);
                    }
                    else if (freePositions.Any(p => p.X == sorted.First().X - 1 && p.Y == sorted.First().Y))
                    {
                        return new Position(sorted.First().Y, sorted.First().X - 1);
                    }
                }
                else if (hitpos.First().X == hitpos.Last().X)
                {
                    var sortedY = hitpos.OrderBy(a => a.Y);

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
            var position = board.Positions.FirstOrDefault(p => p.Y == pos.Y && p.X == pos.X);
            if (position != null && position.symbol != null)
            {
                var ship = ships.First(s => s.Short == position.symbol);
                ship.hits += 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CommunicateShipDestroy(Position position)
        {
            var myPos = board.Positions.First(pos => pos.X == position.X && pos.Y == position.Y);

            if (ships.First(s => s.Short == myPos.symbol).isDestroyed())
                return true;
            else
                return false;
        }

        public void Process(bool isHit, Position position, bool isDestroyed)
        {
            var a = oponentBoard.Positions.First(pos => pos.Y == position.Y && pos.X == position.X);
            a.isAvailable = false;
            if (isDestroyed)
            {
                a.symbol = "X";
                hitpos.Clear();
            }
            else if (isHit)
            {
                a.symbol = "X";
                hitpos.Add(position);
            }
            else
            {
                a.symbol = "M";
            }
        }

    }
}
