﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Of_Life
{
    public class GameOfLifeBase
    {
        internal StringBuilder stringBuilder;
        public int x { get; set; }
        public int y { get; set; }

        public int[,] CurrentCellGeneration { get; set; }
        public int[,] NextCellGeneration { get; set; }

        public GameOfLifeBase()
        {
        }

        public GameOfLifeBase(int x, int y)
        {
            CurrentCellGeneration = new int[x, y];
            NextCellGeneration = new int[x, y];
        }
        public string Draw(int boardSize, int windowWidth)
        {

            string[,] sceneBuffer = new string[boardSize, windowWidth / 2];
            for (int row = 0; row < sceneBuffer.GetLength(0); row++)
            {
                for (int col = 0; col < sceneBuffer.GetLength(1); col++)
                {
                    if (CurrentCellGeneration[row, col] == 1)
                    {
                        sceneBuffer[row, col] = "□";
                    }
                    else
                    {
                        sceneBuffer[row, col] = " ";
                    }
                }
            }
            stringBuilder = new StringBuilder();
            for (int row = 0; row < sceneBuffer.GetLength(0); row++)
            {
                for (int col = 0; col < sceneBuffer.GetLength(1); col++)
                {
                    stringBuilder.Append(sceneBuffer[row, col]);
                }
                stringBuilder.AppendLine();
            }
            DrawMenuPanel(windowWidth);
            return stringBuilder.ToString().TrimEnd();
        }
        public virtual void DrawMenuPanel(int windowWidth)
        {
            stringBuilder.AppendLine(new string('=', windowWidth));
        }
        public void SpawnNextGeneration()
        {
            for (int row = 0; row < NextCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < NextCellGeneration.GetLength(1); col++)
                {
                    int liveNeighbours = CalulateLiveNeighbours(row, col);
                    if (CurrentCellGeneration[row, col] == 1 && liveNeighbours < 2)
                    {
                        NextCellGeneration[row, col] = 0;
                    }
                    else if (CurrentCellGeneration[row, col] == 1 && liveNeighbours > 3)
                    {
                        NextCellGeneration[row, col] = 0;
                    }
                    else if (CurrentCellGeneration[row, col] == 0 && liveNeighbours == 3)
                    {
                        NextCellGeneration[row, col] = 1;
                    }
                    else
                    {
                        NextCellGeneration[row, col] = NextCellGeneration[row, col];
                    }
                }
            }
            TransferNextGenerations();
        }

        private void TransferNextGenerations()
        {
            for (int row = 0; row < CurrentCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentCellGeneration.GetLength(1); col++)
                {
                    CurrentCellGeneration[row, col] = NextCellGeneration[row, col];
                }
            }
        }

        private int CalulateLiveNeighbours(int cellRow, int cellCol)
        {
            int liveNeighbours = 0;

            for (int neighbourCellRow = -1; neighbourCellRow <= 1; neighbourCellRow++)
            {
                for (int neighbourCellCol = -1; neighbourCellCol <= 1; neighbourCellCol++)
                {
                    if (IsOutOfBoundariesOrSameCell(cellRow, cellCol, neighbourCellRow, neighbourCellCol))
                    {
                        continue;
                    }
                    liveNeighbours += CurrentCellGeneration[cellRow + neighbourCellRow, cellCol + neighbourCellCol];

                }
            }
            return liveNeighbours;
        }
        private bool IsOutOfBoundariesOrSameCell(int cellRow, int cellCol, int neighbourCellRow, int neighbourCellCol)
        {
            if (cellRow + neighbourCellRow < 0 || cellRow + neighbourCellRow >= CurrentCellGeneration.GetLength(0))
            {
                return true;
            }
            if (cellCol + neighbourCellCol < 0 || cellCol + neighbourCellCol >= CurrentCellGeneration.GetLength(0))                  // съмнение
            {
                return true;
            }
            if (cellRow + neighbourCellRow == cellRow && cellCol + neighbourCellCol == cellCol)
            {
                return true;
            }
            return false;
        }
    }
}
