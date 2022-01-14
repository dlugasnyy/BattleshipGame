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
            p2.SetShips();
            for (int x = 0; x < 10; x++)
            {
                var s = p.Shoot();
                var c = p2.CheckPosition(s);
                var isde = p2.CommunicateShipDestroy(s);
                p.Process(c, s, isde);

            }


            // Console.WriteLine("Own Board:                           Firing Board:");
        p.ShowBoards();
        p2.ShowBoards();
        }
    }
}
