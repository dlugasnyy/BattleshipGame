using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Battleship.Models;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {

            var p = new Player("p1");
            p.SetShips();
            var p2 = new Player("p2");
            Console.WriteLine("p2 sips:");
            p2.SetShips();
            p2.ShowBoards();
            for (int x = 0; x < 30; x++)
            {
                var s = p.Shoot();
                Console.WriteLine($"{s.X}, {s.Y}");
                var c = p2.CheckPosition(s);
                Console.WriteLine($"{c.ToString()}");
                if (c == true)
                {
                    var isde = p2.CommunicateShipDestroy(s);
                    p.Process(c, s, isde);
                }
                else
                {
                    p.Process(c, s, false);
                }

            }


            // Console.WriteLine("Own Board:                           Firing Board:");
        p.ShowBoards();
        p2.ShowBoards();
        }
    }
}
