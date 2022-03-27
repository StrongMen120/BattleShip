namespace Stateczki.Models
{
    public class Field
    {
        public string TypeField { get; set; }
        public Position Cords { get; set; }

        public Field(int row, int col)
        {
            Cords = new(row, col);
            TypeField = "-";
        }
    }
    public class Position
    {
        public int Xrow { get; set; }
        public int Ycol { get; set; }
        public Position(int row, int col)
        {
            Xrow = row;
            Ycol = col;
        }
    }
}
