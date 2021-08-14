using System;

namespace DartGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Game game = new Game();

            // Kalla StartGame() metoden från Game klassen
            game.StartGame();
        }
    }
}
