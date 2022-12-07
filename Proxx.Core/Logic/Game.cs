using Proxx.Interface;

namespace Proxx.Logic
{
    public class Game : IGame
    {
        private readonly IBoard _board;
        public Game(IBoard board)
        {
            _board = board;
        }
        public void Start()
        {
            _board.Init();
            _board.InitBlackHoles();
            _board.CountAdjacentBlackHoles();
        }
        public ClickStatus Click(int row, int column)
        {
            return _board.Click(row, column);
        }
        public void PrintBoard() 
        {
            Console.WriteLine(_board.ToString());
        }
        public void PrintClick() 
        {
            _board.PrintClick();
        }
    }
}