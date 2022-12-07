using Proxx.Logic;

namespace Proxx.Interface
{
    public interface IGame
    {
        ClickStatus Click(int row, int column);
        void PrintBoard();
        void Start();
    }
}