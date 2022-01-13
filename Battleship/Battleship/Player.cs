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
        //druga tablica

        public Player(string name)
        {
            Initialize();
            Name = name;
            board = new Board();
        }

        private void Initialize()
        {
            ships = new List<Ship>();
            ships.Add(new Ship("Carrier", 5, "C"));
            ships.Add(new Ship("Battleship", 4, "B"));
            ships.Add(new Ship("Cruiser", 3,"c"));
            ships.Add(new Ship("Submarine", 3,"S"));
            ships.Add(new Ship("Destroyer", 2, "D"));
        }

        public void ShowBoards()
        {
            for (int row = 1; row <= 10; row++)
            {
                for (int firstBoard = 1; firstBoard <= 10; firstBoard++)
                {
                    Console.Write("." + "   ");
                }

                Console.Write("                ");
                for (int secondBoard = 1; secondBoard <= 10; secondBoard++)
                {
                    Console.Write("." + "   ");
                }
            }
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
                                x.Y >= startColumn && x.Y <= startColumn + ship.Length && x.X == startRow).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.Y == startColumn + i && pos.X == startRow);
                                x.isAvailable = false;
                                x.symbol = ship.Short;
                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var a = board.Positions.Where(x => x.Y > startColumn - ship.Length && x.Y <= startColumn && x.X == startRow).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.Y == startColumn - i && pos.X == startRow);

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
                                x.X >= startRow && x.X <= startRow + ship.Length && x.Y == startColumn).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.X == startRow + i && pos.Y == startColumn);
                                x.isAvailable = false;
                                x.symbol = ship.Short;

                            }
                            isShipSet = true;
                        }
                        else
                        {
                            var a = board.Positions.Where(x => x.X > startRow - ship.Length && x.X <= startRow && x.Y == startColumn).ToList();
                            if (a.Any(x => !x.isAvailable))
                            {
                                isShipSet = false;
                                continue;
                            }
                            for (int i = 0; i < ship.Length; i++)
                            {
                                var x = board.Positions.First(pos => pos.X == startRow - i && pos.Y == startColumn);
                                x.isAvailable = false;
                                x.symbol = ship.Short;

                            }
                            isShipSet = true;

                        }

                    }
                }

            }
        }
    }
}
