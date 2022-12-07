using Proxx.Interface;
using System.Runtime.CompilerServices;
using System.Text;

namespace Proxx.Logic
{
    public class Board : IBoard
    {
        public int[,] GameBoard
        {
            get { return _board; }
            set { _board = value; }

        }
        private bool IsBlackHoleClicked = false;
        private readonly int _size;
        private readonly int _numberOfblackHoles;

        /// <summary>
        /// 
        /// get => O(1) ST
        /// set => O(1) ST
        /// init => O(1) ST
        /// 
        /// Immutable array, representation of board with rows and columns
        /// 
        /// </summary>
        private int[,] _board;

        /// <summary>
        /// 
        /// get => O(1) ST
        /// set => O(1) ST
        /// init => O(1) ST
        /// 
        /// Dynamic array, representation of coordinates for Black Holes
        /// 
        /// </summary>
        private readonly Queue<ValueTuple<int, int>> _blackHoles;

        /// <summary>
        /// 
        /// get => O(1) ST
        /// set => O(1) ST
        /// init => O(1) ST
        /// 
        /// Collection which allow to us save coordinates as key and boolean value which means is cell visited
        /// 
        /// </summary>
        private readonly Dictionary<ValueTuple<int, int>, bool> _visitedBoardCells;

        /// <summary>
        /// 
        /// We can assume O(b+h+c) Space and Time complexity, where 
        ///    b dimenssions of the board
        ///    h number of black holes
        ///    c number of cells
        /// 
        /// But  b,h,c - are constant values, and as we know in terms of Big 0 notation constant we can skip
        /// 
        /// 0(1) Space and Time complexity
        /// 
        /// </summary>
        public Board(int size, int numberOfblackHoles)
        {
            _size = size;
            _numberOfblackHoles = numberOfblackHoles;

            _board = new int[_size, _size];                                     // O(b) ST => O(1)
            _blackHoles = new Queue<ValueTuple<int, int>>(numberOfblackHoles);       // O(h) ST => O(1)
            _visitedBoardCells = new Dictionary<ValueTuple<int, int>, bool>(_size * _size); // O(c) ST => O(1)
        }

        /// <summary>
        /// 
        /// O(n^2) Time complexity, where n size of board
        /// O(1) Space complexity, where n size of board
        /// 
        /// </summary>
        public void Init()
        {
            for (int row = 0; row < _size; row++) // O(n) T
            {
                for (int col = 0; col < _size; col++) // O(n) T
                {
                    _board[row, col] = 0; // 0(1) ST
                }
            }
        }

        /// <summary>
        /// 
        /// O(n) Best Time complexity, where n is number of black holes
        /// O(n + m) Average Time complexity, where n is number of black holes and m is number of duplicates
        ///
        /// 
        /// O(n) Space complexity, where n is number of blackholes
        /// 
        /// </summary>
        public void InitBlackHoles()
        {
            var randomizer = new Random(); // O(1) ST

            while (_blackHoles.Count != _numberOfblackHoles) // Loop O(n+m) T
            {
                var row = randomizer.Next(0, _size); // O(1) ST
                var col = randomizer.Next(0, _size); // O(1) ST

                var blackHoleCoordinates = new ValueTuple<int, int>(row, col); // O(1) ST

                if (!_blackHoles.Contains(blackHoleCoordinates)) // O(1) T 
                {
                    _blackHoles.Enqueue(blackHoleCoordinates); //Queue set => 0(1) ST
                    _board[row, col] = -1;                      //Array set => 0(1) ST
                }
            }
        }
     
        /// <summary>
        /// 
        /// O(n^2) Time complexity
        /// O(1) Space complexity
        /// 
        /// </summary>
        public void CountAdjacentBlackHoles()
        {
            for (var row = 0; row < _size; row++)     // O(n) Time
            {
                for (var col = 0; col < _size; col++) // O(n) Time
                {
                    if (_board.IsHole(row, col))
                    {
                        continue;
                    }

                    _board.CountAdjacentHoles(row, col); //O(1) ST
                }
            }
        }
       
        /// <summary>
        /// 
        /// O(1)   Best Time complexity, when click directly on hole or number > 0 
        /// O(n)   Average Time complexity, when click directly on 0 and neighbors around clicked cell > 0
        /// O(n^2) Worst Time complexity when we have to traverse all cells in the board
        /// 
        /// 
        /// O(1) Space complexity
        /// 
        /// </summary>
        public ClickStatus Click(int row, int column)
        {
            if (_board.IsHole(row, column) || IsBlackHoleClicked) // O(1) ST
            {
                IsBlackHoleClicked = true;
                return ClickStatus.GameOver;
            }

            if (IsGameComplited())
            {
                return ClickStatus.Completed;
            }

            var clickCoordinate = new ValueTuple<int, int>(row, column); //O(1) ST

            _visitedBoardCells.TryGetValue(clickCoordinate, out var isCellVisited); //O(1) ST

            if (isCellVisited)
            {
                return ClickStatus.CellVisited;
            }

            _visitedBoardCells.Add(clickCoordinate, true); // O(1) ST

            if (!_board.IsZero(row, column)) // O(1) ST
            {
                return ClickStatus.Continue;
            }

            var neighbors = _board.GetNeighbors(row, column); // O(1) Time, O(n) Space - where n is number of neighbors with value >= 0

            neighbors.ForEach(neighbor => { Click(neighbor.Item1, neighbor.Item2); }); // O(n) Time, O(1) Space

            return ClickStatus.Continue;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    builder.Append(_board.IsHole(row, col) ? " " + _board[row, col] : "  " + _board[row, col]);
                }
                builder.AppendLine(Environment.NewLine);
            }

            return builder.ToString();
        }
        public void PrintClick()
        {

            StringBuilder builder = new StringBuilder();

            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    var isVisited = false;

                    _visitedBoardCells.TryGetValue(new ValueTuple<int, int>(row, col), out isVisited);

                    if (isVisited)
                    {
                        builder.Append(_board.IsHole(row, col) ? " *" : "  *");
                    }
                    else
                    {
                        builder.Append(_board.IsHole(row, col) ? " " + _board[row, col] : "  " + _board[row, col]);
                    }
                }
                builder.AppendLine(Environment.NewLine);
            }

            Console.WriteLine(builder.ToString());
        }
        private bool IsGameComplited()
        {
            return _visitedBoardCells.Count == (_size * _size - _numberOfblackHoles);
        }
    }
}
