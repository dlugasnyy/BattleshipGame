using System;

namespace Battleship.Models
{
    public interface IBoards
    {
        void Show();
    }
   public class Boards :IBoards
    {
        public Board Board { get; private set; }
        public Board OpponentBoard { get; private set; }

        public Boards()
        {
            Board = new Board();
            OpponentBoard = new Board();
        }
        public void Show()
        {
            for (int row = 1; row <= 10; row++)
            {
                for (int playerBoardColumn = 1; playerBoardColumn <= 10; playerBoardColumn++)
                {
                    var position = Board.GetPositionByCoords(row, playerBoardColumn);
                    if (position.IsAvailable)
                    {
                        Console.Write("." + "   ");
                    }
                    else
                    {
                        Console.Write($"{position.Symbol}" + "   ");

                    }

                }
                Console.Write("           ");
                for (int hitBoardColumn = 1; hitBoardColumn <= 10; hitBoardColumn++)
                {
                    var position = OpponentBoard.GetPositionByCoords(row, hitBoardColumn);

                    if (position.IsAvailable)
                    {
                        Console.Write("." + "  ");
                    }
                    else
                    {
                        if (position.Symbol == "X")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{position.Symbol}" + "  ");
                            Console.ResetColor();
                        }
                        else if (position.Symbol == "M")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{position.Symbol}" + "  ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{position.Symbol}" + "   ");
                        }
                    }
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
