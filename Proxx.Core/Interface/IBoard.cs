using Proxx.Logic;

namespace Proxx.Interface
{
    public interface IBoard
    {
        public int[,] GameBoard { get; set; }
        void Init();
        void InitBlackHoles();
        void CountAdjacentBlackHoles();
        ClickStatus Click(int row, int column);
        void PrintClick();
    }
}