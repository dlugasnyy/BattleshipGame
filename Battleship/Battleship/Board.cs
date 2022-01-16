using System.Collections.Generic;
using System.Linq;
using Battleship.Models;

namespace Battleship
{
    // TODO Move to Models
    public class Board
    {
        public const int MaxRows = 10;
        public const int MaxColumns = 10;
        public List<Position> Positions { get; set; }

        public List<Position> AvailablePositions
        {
            get { return Positions.Where(pos => pos.IsAvailable).ToList(); }
        }

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
            return Positions.FirstOrDefault(x => x.X == column && x.Y == row);
        }

        public List<Position> GetHorizontalPositions(int startColumn, int endColumn, int row)
        {
            return Positions.Where(x => x.X >= startColumn && x.X <= endColumn && x.Y == row).ToList();
        }

        public List<Position> GetVerticalPositions(int startRow, int endRow, int column)
        {
            return Positions.Where(x => x.Y >= startRow && x.Y <= endRow && x.X == column).ToList();
        }
    }
}
