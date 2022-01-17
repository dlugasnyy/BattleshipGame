namespace Battleship.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAvailable { get; set; }
        public string Symbol { get; set; }

        public Position()
        {
            
        }

        public Position(int y, int x)
        {
            Y = y;
            X = x;
            IsAvailable = true;
        }
    }
}