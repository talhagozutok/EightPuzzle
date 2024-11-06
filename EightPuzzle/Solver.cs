namespace EightPuzzle;
public abstract class Solver
{
    protected readonly int[,] GoalState = new int[3, 3]
    {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 }
    };

    protected int MaxFringeSize = 0;
    protected int NodesExpanded = 0;
    protected int MaxSearchDepth = 0;

    public abstract void Solve(State initialState, bool printResults = true);
    
    protected List<State> GenerateChildrenStates(State currentState)
    {
        var children = new List<State>();

        // Generate possible moves and add valid ones
        var directions = new (int dx, int dy, string move)[]
        {
            (0, -1, "left"),
            (0, 1, "right"),
            (-1, 0, "up"),
            (1, 0, "down")
        };

        foreach (var (dx, dy, move) in directions)
        {
            var childState = currentState.MoveZero(dx, dy);
            if (childState is not null)
            {
                childState.LastMove = move;
                childState.Parent = currentState;
                childState.SearchDepth = currentState.SearchDepth + 1;
                children.Add(childState);
            }
        }

        return children;
    }

    protected void PrintResults(State finalState)
    {
        var path = FindPath(finalState);
        path.Reverse(); // Reverse the path to get it from the initial state to the final state

        Console.WriteLine("Solution Moves (moves of the zero tile only): ");
        Console.WriteLine(string.Join(", ", path));

        Console.WriteLine($"\nCost of path: {path.Count}");
        Console.WriteLine($"Nodes expanded: {NodesExpanded}");
        Console.WriteLine($"Max fringe size: {MaxFringeSize}");
        Console.WriteLine($"Max search depth: {MaxSearchDepth}");
    }

    private static List<string> FindPath(State state)
    {
        var path = new List<string>();
        while (state.Parent is not null)
        {
            path.Add(state.LastMove);
            state = state.Parent;
        }
        return path;
    }
}