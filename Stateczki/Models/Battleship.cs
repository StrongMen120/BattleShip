using System;

namespace Stateczki.Models
{
    public class Battleship
    {
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public Battleship(int boardSize)
        {
            P1 = new Player("Player1", boardSize);
            P2 = new Player("Player2", boardSize);
        }
        public void StartGame()
        {
            while (!P1.CheckLost && !P2.CheckLost)
            {
                var pos = P1.Shot();
                var res = P2.CheckShot(pos);
                P1.DrawShotResult(pos, res);
                if (!P2.CheckLost)
                {
                    pos = P2.Shot();
                    res = P1.CheckShot(pos);
                    P2.DrawShotResult(pos, res);
                }
            }
            if (P1.CheckLost) {
                Console.WriteLine(P2.NamePlayer + " wygrywa gre!");
            }
            else if (P2.CheckLost) {
                Console.WriteLine(P1.NamePlayer + " wygrywa gre!");
            }
        }
    }
}
