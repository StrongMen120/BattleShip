using Stateczki.Models;
using System;

namespace Stateczki
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Witaj w symulacji gry w statki!");
            Console.ReadKey();
            Battleship bt = new(12); // batleship size 12
            bt.P1.ShowBoards();
            bt.P2.ShowBoards();
            bt.StartGame();
            bt.P1.ShowBoards();
            bt.P2.ShowBoards();
            Console.ReadKey();
        }
    }
}
