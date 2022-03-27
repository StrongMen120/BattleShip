using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateczki.Models
{
    public class Player
    {
        public string NamePlayer { get; set; }
        public int BoardSize { get; set; }
        public MyBoard MyBoard { get; set; }
        public EnemyBoard EnemyBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool CheckLost
        {
            get { return Ships.All(x => x.IsSunk); }
        }
        public Player(string name, int boardSize)
        {
            NamePlayer = name;
            Ships = new();
            Ships.Add(new(5, "5-1"));
            Ships.Add(new(4, "4-1"));
            Ships.Add(new(4, "4-2"));
            Ships.Add(new(3, "3-1"));
            Ships.Add(new(3, "3-2"));
            Ships.Add(new(2, "2-1"));
            Ships.Add(new(2, "2-2"));
            Ships.Add(new(2, "2-3"));
            MyBoard = new MyBoard(boardSize);
            EnemyBoard = new EnemyBoard(boardSize);
            BoardSize = boardSize;
            SetShips();
        }
        // Set position all ships
        public void SetShips()
        {
            Random r = new();
            foreach (var ship in Ships)
            {
                bool isOpen = true;
                while (isOpen)
                {
                    int iStartC = r.Next(1, BoardSize + 1);
                    int iStartR = r.Next(1, BoardSize + 1);
                    int iEndC = iStartC, iEndR = iStartR;
                    bool IsHorizontal = r.Next(0, 2) == 0; //0(true)-Horizontal //1(false)-Vertical
                    if (IsHorizontal) {//Drawing Horizontal Ship
                        iEndR += ship.Size - 1;
                        if (iEndR > BoardSize) {
                            continue;//Ship outside board
                        }
                    }
                    else {//Drawing Vertical Ship
                        iEndC += ship.Size - 1;
                        if (iEndC > BoardSize) {
                            continue;//Ship outside board
                        }
                    }
                    //Take range ship and check if on this range dont have board
                    var myBoard = MyBoard.BoardFields.Where(p => p.Cords.Xrow >= iStartR && p.Cords.Xrow <= iEndR && p.Cords.Ycol >= iStartC && p.Cords.Ycol <= iEndC);
                    if (myBoard.Any(x => x.TypeField != "-")) {
                        continue;
                    }
                    foreach (var panel in myBoard) {
                        panel.TypeField = ship.NameShip;
                    }
                    isOpen = false;
                }
            }
        }
        public void ShowBoards() //Write to console board
        {
            Console.WriteLine(NamePlayer + " Moja Plansza:              Przeciwnika Plansza:");
            for (int row = 1; row <= BoardSize; row++)
            {
                for (int col = 1; col <= BoardSize; col++)
                {
                    Console.Write(MyBoard.BoardFields.FirstOrDefault(p => p.Cords.Xrow == row && p.Cords.Ycol == col).TypeField[0] + " "); 
                }
                Console.Write("                  ");
                for (int col = 1; col <= BoardSize; col++)
                {
                    Console.Write(EnemyBoard.BoardFields.FirstOrDefault(p => p.Cords.Xrow == row && p.Cords.Ycol == col).TypeField[0] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public Position Shot()  //Shot to enemy empty fields
        {
            var hitNeighbors = EnemyBoard.GetHitNextField();
            Position pos;
            if (hitNeighbors.Any())
            {
                Random rand = new();
                var Panels = EnemyBoard.GetHitNextField();
                int randomPosition = rand.Next(Panels.Count);
                pos = Panels[randomPosition].Cords;
            }
            else
            {
                var Panels = EnemyBoard.GetOpenRandomPanels();
                Random rand = new();
                var randomPosition = rand.Next(Panels.Count);
                pos = Panels[randomPosition];
            }
            Console.WriteLine(NamePlayer + " Strzela w :[" + pos.Xrow.ToString() + ", " + pos.Ycol.ToString() + "]");
            return pos;
        }
        public bool CheckShot(Position pos) //Check if hits shift or not
        {
            var field = MyBoard.BoardFields.FirstOrDefault(p => p.Cords.Xrow == pos.Xrow && p.Cords.Ycol == pos.Ycol);
            if (field.TypeField == "-")
            {
                Console.WriteLine(NamePlayer + " : Pudło!");
                return false;
            }
            var ship = Ships.First(x => x.NameShip == field.TypeField);
            ship.Hits++;
            Console.WriteLine(NamePlayer + " : Trafiony!");
            if (ship.IsSunk) {
                Console.WriteLine(NamePlayer + " : Trafiony zatapiony statek z : " + ship.Size + "-masztami !");
            }
            return true;
        }

        public void DrawShotResult(Position pos, bool IsHit) //Check if hits shift or not
        {
            var field = EnemyBoard.BoardFields.FirstOrDefault(p => p.Cords.Xrow == pos.Xrow && p.Cords.Ycol == pos.Ycol);
            if (IsHit) {
                field.TypeField = "H";
            }
            else {
                field.TypeField = "M";
            }
        }
    }
}
