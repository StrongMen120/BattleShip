namespace Stateczki.Models
{
    public class Ship
    {
        public int Size { get; set; }
        public int Hits { get; set; }
        public string NameShip { get; set; }    // Name Ship
        public bool IsSunk {    //Ship is Sunk
            get { return Hits >= Size; }
        }
        public Ship(int width, string name)
        {
            Size = width;
            NameShip = name;
        }
    }
}
