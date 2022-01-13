using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Models;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {

            // var d = new Board();
            var p = new Player("p1");
            p.SetShips();
            // d.InitializePanels();
            // Console.WriteLine("Own Board:                           Firing Board:");
            for (int row = 1; row <= 10; row++)
            {
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    var pan = p.board.GetPositionFromBoard(row, ownColumn);
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
                    Console.Write("." + "   ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
