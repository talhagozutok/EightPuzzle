using EightPuzzle;

ProblemGenerator generator = new();
Board randomBoard = generator.GenerateRandomBoard();

Console.WriteLine("Generated Puzzle:");
PrintBoard(randomBoard);

State initialState = new(
    currentBoard: randomBoard,
    parent: null,
    lastMove: "",
    searchDepth: 0
);

BfsSolver solver = new();
solver.Solve(initialState);

void PrintBoard(Board board)
{
    for (int i = 0; i < board.Tiles.GetLength(0); i++)
    {
        for (int j = 0; j < board.Tiles.GetLength(1); j++)
        {
            Console.Write(board.Tiles[i, j] + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
