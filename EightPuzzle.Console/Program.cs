using EightPuzzle;

int[,] initialBoard = {
    { 7, 2, 4 },
    { 5, 0, 6 },
    { 8, 3, 1 }
};

Board board = new(initialBoard);

var zeroPosition = board.IndexOfZero();
Console.WriteLine($"Index of zero: ({zeroPosition.Item1}, {zeroPosition.Item2})");

int[,] sameBoard = {
    { 7, 2, 4 },
    { 5, 0, 6 },
    { 8, 3, 1 }
};
Console.WriteLine("Is equal to sameBoard: " + board.IsEqual(sameBoard));

int[,] differentBoard = {
    { 1, 2, 3 },
    { 4, 5, 0 },
    { 6, 7, 8 }
};
Console.WriteLine("Is equal to differentBoard: " + board.IsEqual(differentBoard));

Console.WriteLine("Hashcode: " + board.GetHashCode());

Board anotherBoard = new(sameBoard);
Console.WriteLine("Equals anotherBoard: " + board.Equals(anotherBoard));
