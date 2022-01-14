using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Battleship.Models
{
    public class Ship
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int hits { get; set; }
        public string Short { get; set; }
        private List<Position> positions { get; set; }

        public Ship(string name, int length, string shortName)
        {
            Name = name;
            Length = length;
            Short = shortName;
        }
        public bool isDestroyed()
        {
            return hits >= Length;
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool isAvailable { get; set; }
        public string symbol { get; set; }

        public Position()
        {
            
        }
        public Position(int y, int x)
        {
            Y = y;
            X = x;
            isAvailable = true;
        }
    }

    public class AimingPosition : Position
    {
        public bool wasHitted { get; set; }
    }
}
