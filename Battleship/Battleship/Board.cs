using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleship.Models;

namespace Battleship
{
    public class Board
    {
        public List<Position> Positions { get; set; }
        public const int MaxRows = 10;
        public const int MaxColumns = 10;
        public Board()
        {
            InitializePanels();
        }

        public void InitializePanels()
        {
            Positions = new List<Position>();
            for (int i = 1; i <= MaxRows; i++)
            {
                for (int j = 1; j <= MaxColumns; j++)
                {
                    Positions.Add(new Position(i, j));
                }
            }
        }

        public Position GetPositionFromBoard(int row, int column)
        {
            return Positions.First(x => x.Y == row && x.X == column);
        }
    }
}
