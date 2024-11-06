using EightPuzzle;

// Define the initial state of the puzzle
int[,] startState = {
    { 7, 2, 4 },
    { 5, 0, 6 },
    { 8, 3, 1 }
};

Board board = new(startState);
State initialState = new(
    currentBoard: board,
    parent: null,
    lastMove: "",
    searchDepth: 0
);

BfsSolver solver = new();
solver.Solve(initialState);
