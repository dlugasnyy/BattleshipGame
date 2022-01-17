
namespace Battleship.Models
{
    public class Ship
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int Hits { get; set; }
        public string Short { get; set; }

        public Ship(string name, int length, string shortName)
        {
            Name = name;
            Length = length;
            Short = shortName;
        }

        public bool isDestroyed()
        {
            return Hits >= Length;
        }
    }
}
