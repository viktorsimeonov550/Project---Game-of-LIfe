using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Of_Life
{
    public class GameOfLifeEditor : GameOfLifeBase
    {
        private int cursorPositionX = 0;
        private int cursorPositionY = 0;

        private int cellPositionX = 0;
        private int cellPositionY = 0;

        public GameOfLifeEditor(int x, int y) : base(x, y)    //Class GameOfLifeBuiltln
        {
        }
        public override void DrawMenuPanel(int windowWidth)
        {
            base.DrawMenuPanel(windowWidth);
            stringBuilder.AppendLine("[Arrow keys] to move             [Enter] Clear the board");
            stringBuilder.AppendLine("[Spacebar] Toggle cell           [Escape] Start menu");
            stringBuilder.AppendLine("[Backspace] Start/Stop the life");
        }
        public string PlayerMove(ConsoleKeyInfo key, int sizeOfBoard, int windowWidth)
        {
            string genaretionToReturn = "";
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (cursorPositionX - 2 > 0)
                    {
                        Console.SetCursorPosition(cursorPositionX -= 2, cursorPositionY);
                        cellPositionY--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorPositionX + 2 < CurrentCellGeneration.GetLength(1) - 1)
                    {
                        Console.SetCursorPosition(cursorPositionX += 2, cursorPositionY);
                        cellPositionY++;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (cellPositionX < CurrentCellGeneration.GetLength(0) - 1)
                    {
                        Console.SetCursorPosition(cursorPositionX, cursorPositionY += 2);
                        cellPositionX--;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (cellPositionX > 0)
                    {
                        Console.SetCursorPosition(cursorPositionX, cursorPositionY += 2);
                        cellPositionX++;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    ToggleCurrentCellState();
                    genaretionToReturn = Draw(sizeOfBoard, windowWidth);
                    break;
                case ConsoleKey.Enter:
                    ClearBoard();
                    genaretionToReturn = Draw(sizeOfBoard, windowWidth);
                    break;

                default:
                    genaretionToReturn = Draw(sizeOfBoard, windowWidth);
                    break;
            }
            return genaretionToReturn;
        }
        private void ToggleCurrentCellState()
        {
            if (CurrentCellGeneration[cellPositionX, cellPositionY] == 1)
            {
                CurrentCellGeneration[cellPositionX, cellPositionY] = 0;
            }
            else
            {
                CurrentCellGeneration[cellPositionX, cellPositionY] = 1;
            }
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);

        }
        private void ClearBoard()
        {
            for (int row = 0; row < CurrentCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentCellGeneration.GetLength(1); col++)
                {
                    CurrentCellGeneration[row, col] = 0;
                }
            }
        }

    }
}