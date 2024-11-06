using EightPuzzle;

int numProblems = 10;
Console.WriteLine($"Running benchmark on {numProblems} random 8-puzzle problems...\n");

var benchmark = new PuzzleSolverBenchmark();
benchmark.RunBenchmark(numProblems);

Console.WriteLine("\nBenchmark completed.");
