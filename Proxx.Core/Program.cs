using Proxx.Logic;

int boardSize = 8;
int numberOfBlackHole = 10;

var game = new Game(new Board(boardSize, numberOfBlackHole));

game.Start();

game.PrintBoard();

var status = game.Click(4, 4);
Console.WriteLine($"click 1: {status}");

status = game.Click(0, 1);
Console.WriteLine($"click 2: {status}");


game.PrintClick();