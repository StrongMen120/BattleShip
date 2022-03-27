using System.Collections.Generic;
using System.Linq;

namespace Stateczki.Models
{
    public class MyBoard
    {
        public List<Field> BoardFields { get; set; }
        public int Size { get; set; }
        public MyBoard(int size)
        {
            BoardFields = new List<Field>();
            Size = size;
            for (int i = 1; i <= size; i++)
            {
                for (int j = 1; j <= size; j++)
                {
                    BoardFields.Add(new Field(i, j));
                }
            }
        }
    }
    public class EnemyBoard : MyBoard
    {
        public EnemyBoard(int size) : base(size)
        {
            
        }
        //Get all dont shot fields
        public List<Position> GetOpenRandomPanels()
        {
            return BoardFields.Where(p => p.TypeField == "-").Select(a => a.Cords).ToList();
        }
        //checking all neighbors
        public List<Field> GetHitNextField()
        {
            List<Field> lstFieldsHit = new();
            var hits = BoardFields.Where(x => x.TypeField == "H");
            foreach (var hit in hits)
            {
                lstFieldsHit.AddRange(GetNextField(hit.Cords).ToList());
            }
            return lstFieldsHit.Distinct().Where(x => x.TypeField == "-").ToList();
        } 
        public List<Field> GetNextField(Position v)
        {
            int row = v.Xrow, col = v.Ycol;
            List<Field> fields = new();
            if (col > 1) {
                fields.Add(BoardFields.FirstOrDefault(p => p.Cords.Xrow == row && p.Cords.Ycol == col - 1));
            }
            if (row > 1) {
                fields.Add(BoardFields.FirstOrDefault(p => p.Cords.Xrow == row - 1 && p.Cords.Ycol == col));
            }
            if (row < Size) {
                fields.Add(BoardFields.FirstOrDefault(p => p.Cords.Xrow == row + 1 && p.Cords.Ycol == col));
            }
            if (col < Size) {
                fields.Add(BoardFields.FirstOrDefault(p => p.Cords.Xrow == row && p.Cords.Ycol == col + 1));
            }
            return fields;
        }
    }
}
