namespace Proxx.Logic
{
    public static class BoardHelper
    {
        public static List<Tuple<int,int>> GetNeighbors(this int[,] board, int row, int col)
        {
            var neighbors = new List<Tuple<int, int>>(8);

            //left
            if (board.IsNotHole(row, col - 1))
            {
                neighbors.Add(new Tuple<int, int>(row , col - 1));
            }

            //left top diagonale
            if (board.IsNotHole(row - 1, col - 1))
            {
                neighbors.Add(new Tuple<int, int>(row - 1, col - 1));
            }

            //top
            if (board.IsNotHole(row - 1, col))
            {
                neighbors.Add(new Tuple<int, int>(row - 1, col));
            }

            //right top diagonale
            if (board.IsNotHole(row - 1, col + 1))
            {
                neighbors.Add(new Tuple<int, int>(row - 1, col + 1));
            }

            //right
            if (board.IsNotHole(row, col + 1))
            {
                neighbors.Add(new Tuple<int, int>(row, col + 1));
            }

            //right bottom diagonale
            if (board.IsNotHole(row + 1, col + 1))
            {
                neighbors.Add(new Tuple<int, int>(row + 1, col + 1));
            }

            //bottom    
            if (board.IsNotHole(row + 1, col))
            {
                neighbors.Add(new Tuple<int, int>(row + 1, col));
            }
            
            //bottom left diagonale
            if (board.IsNotHole(row + 1, col - 1))
            {
                neighbors.Add(new Tuple<int, int>(row + 1, col - 1));
            }

            return neighbors;
        }
        public static void CountAdjacentHoles(this int[,] board, int row, int col)
        {
            //left
            if (board.IsHole(row, col - 1))
            {
                IncreaseHoleCount(board, row, col);
            }

            //left top diagonale
            if (board.IsHole(row - 1, col - 1))
            {
                IncreaseHoleCount(board, row, col);
            }

            //top
            if (board.IsHole(row - 1, col))
            {
                IncreaseHoleCount(board, row, col);
            }

            //right top diagonale
            if (board.IsHole(row - 1, col + 1))
            {
                IncreaseHoleCount(board, row, col);
            }

            //right
            if (board.IsHole(row, col + 1))
            {
                IncreaseHoleCount(board, row, col);
            }

            //right bottom diagonale
            if (board.IsHole(row + 1, col + 1))
            {
                IncreaseHoleCount(board, row, col);
            }

            //bottom
            if (board.IsHole(row + 1, col))
            {
                IncreaseHoleCount(board, row, col);
            }

            //bottom left diagonale
            if (board.IsHole(row + 1, col - 1))
            {
                IncreaseHoleCount(board, row, col);
            }
        }
        public static bool IsHole(this int[,] board, int row, int col)
        {

            return IsValidLimits(board, row, col) && board[row,col] == -1;
        }
        public static bool IsNotHole(this int[,] board, int row, int col)
        {
            return IsValidLimits(board, row, col) && board[row, col] >= 0;
        }
        public static bool IsZero(this int[,] board, int row, int col)
        {
            return IsValidLimits(board, row, col) && board[row, col] == 0;
        }
        static void IncreaseHoleCount(int[,] board, int row, int col)
        {
            board[row,col] = board[row,col] + 1;
        }
        static bool IsValidLimits(int[,] board, int row, int col)
        {
            var leftLimits = row >= 0 && col >= 0;
            var rightLimits = row < board.GetLength(0) && col < board.GetLength(1);

            return leftLimits && rightLimits;
        }
    }
}
