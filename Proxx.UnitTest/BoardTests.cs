using Proxx.Interface;
using Proxx.Logic;

namespace Prox.UnitTest
{
    public class BoardTests
    {
        IBoard fullGameBoard;
        IBoard boardWithBlackHoles;

        [SetUp]
        public void Setup()
        {
            fullGameBoard = new Board(4, 4);
            fullGameBoard.GameBoard = GetFullBoard();

            boardWithBlackHoles = new Board(4, 4);
            boardWithBlackHoles.GameBoard = GetBlackHoleBoard();
        }

        [Test]
        public void BoardClicks_success()
        {
            var continueStatus = fullGameBoard.Click(0, 0);
            var cellVisitedStatus = fullGameBoard.Click(0, 0);
            var gameOver = fullGameBoard.Click(0, 1);

            Assert.That(continueStatus, Is.EqualTo(ClickStatus.Continue));
            Assert.That(cellVisitedStatus, Is.EqualTo(ClickStatus.CellVisited));
            Assert.That(gameOver, Is.EqualTo(ClickStatus.GameOver));
        }

        [Test]
        public void BoardClicks_fail()
        {
            var continueStatus = fullGameBoard.Click(0, 0);
            var cellVisitedStatus = fullGameBoard.Click(0, 0);
            var gameOver = fullGameBoard.Click(0, 1);

            Assert.That(continueStatus, Is.Not.EqualTo(ClickStatus.GameOver));
            Assert.That(cellVisitedStatus, Is.Not.EqualTo(ClickStatus.Continue));
            Assert.That(gameOver, Is.Not.EqualTo(ClickStatus.CellVisited));
        }

        [Test]
        public void CountAdjacentBlackHoles_success()
        {
            boardWithBlackHoles.CountAdjacentBlackHoles();

            Assert.That(fullGameBoard.GameBoard[0, 0], Is.EqualTo(2));
            Assert.That(fullGameBoard.GameBoard[0, 2], Is.EqualTo(3));
            Assert.That(fullGameBoard.GameBoard[0, 3], Is.EqualTo(1));

            Assert.That(fullGameBoard.GameBoard[2, 1], Is.EqualTo(3));
            Assert.That(fullGameBoard.GameBoard[2, 2], Is.EqualTo(3));

            Assert.That(fullGameBoard.GameBoard[3, 3], Is.EqualTo(0));
        }

        private int[,] GetFullBoard()
        {
            var _board = new int[4, 4];

            _board[0, 0] = 2;
            _board[0, 1] = -1;
            _board[0, 2] = 3;
            _board[0, 3] = 1;

            _board[1, 0] = 2;
            _board[1, 1] = -1;
            _board[1, 2] = -1;
            _board[1, 3] = 1;

            _board[2, 0] = 2;
            _board[2, 1] = 3;
            _board[2, 2] = 3;
            _board[2, 3] = 1;

            _board[3, 0] = 1;
            _board[3, 1] = -1;
            _board[3, 2] = 1;
            _board[3, 3] = 0;

            return _board;
        }

        private int[,] GetBlackHoleBoard()
        {
            var _board = new int[4, 4];

            _board[0, 0] = 0;
            _board[0, 1] = -1;
            _board[0, 2] = 0;
            _board[0, 3] = 0;

            _board[1, 0] = 0;
            _board[1, 1] = -1;
            _board[1, 2] = -1;
            _board[1, 3] = 0;

            _board[2, 0] = 0;
            _board[2, 1] = 0;
            _board[2, 2] = 0;
            _board[2, 3] = 0;

            _board[3, 0] = 0;
            _board[3, 1] = -1;
            _board[3, 2] = 0;
            _board[3, 3] = 0;

            return _board;
        }
    }
}