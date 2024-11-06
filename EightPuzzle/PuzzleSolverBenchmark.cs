using System.Diagnostics;

namespace EightPuzzle;
public class PuzzleSolverBenchmark
{
    private record PerformanceMetrics(double TimeTaken, int NodesExpanded, int MaxFringeSize, int MaxSearchDepth);

    // Run 10 different problems by default
    public void RunBenchmark(int numProblems = 10)
    {
        var solvers = new Dictionary<string, Solver>
            {
                { "BFS", new BfsSolver() },
                { "DFS", new DfsSolver() },
                { "Depth-Limited (20)", new DepthLimitedSolver(100) },
                { "Iterative Deepening", new IterativeDeepeningSolver() }
            };

        // Generate random problems
        var generator = new ProblemGenerator();
        var problems = new List<State>();
        for (int i = 0; i < numProblems; i++)
        {
            var randomBoard = generator.GenerateRandomBoard();
            problems.Add(new State(randomBoard, null, "", 0));
        }

        // Initialize metrics
        var cumulativeMetrics = new Dictionary<string, List<PerformanceMetrics>>();
        foreach (var solverName in solvers.Keys)
        {
            cumulativeMetrics[solverName] = [];
        }

        // Run each solver on each problem
        foreach (var problem in problems)
        {
            foreach (var (solverName, solver) in solvers)
            {
                var metrics = MeasurePerformance(solver, problem);
                cumulativeMetrics[solverName].Add(metrics);
            }
        }

        foreach (var (solverName, metricsList) in cumulativeMetrics)
        {
            var averageTime = ComputeAverage(metricsList, m => m.TimeTaken);
            var averageNodesExpanded = (int)ComputeAverage(metricsList, m => m.NodesExpanded);
            var averageMaxFringeSize = (int)ComputeAverage(metricsList, m => m.MaxFringeSize);
            var averageMaxSearchDepth = (int)ComputeAverage(metricsList, m => m.MaxSearchDepth);

            Console.WriteLine($"Average performance for {solverName}:");
            Console.WriteLine($"  Time taken: {averageTime:F4} seconds");
            Console.WriteLine($"  Nodes expanded: {averageNodesExpanded}");
            Console.WriteLine($"  Max fringe size: {averageMaxFringeSize}");
            Console.WriteLine($"  Max search depth: {averageMaxSearchDepth}");
            Console.WriteLine(new string('-', 50));
        }
    }

    private PerformanceMetrics MeasurePerformance(Solver solver, State initialState)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        solver.Solve(initialState, printResults: false);

        stopwatch.Stop();

        // Collect performance metrics
        return new PerformanceMetrics(
            TimeTaken: stopwatch.Elapsed.TotalSeconds,
            NodesExpanded: solver.NodesExpanded,
            MaxFringeSize: solver.MaxFringeSize,
            MaxSearchDepth: solver.MaxSearchDepth
        );
    }

    private double ComputeAverage(List<PerformanceMetrics> metricsList, Func<PerformanceMetrics, double> selector)
    {
        double sum = 0;
        foreach (var metrics in metricsList)
        {
            sum += selector(metrics);
        }
        return sum / metricsList.Count;
    }
}
