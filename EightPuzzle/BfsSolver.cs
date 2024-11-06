using System.Text;

namespace EightPuzzle;

public class BfsSolver
{
    private readonly int[,] GoalState = new int[3, 3]
    {
        { 0, 1, 2 },
        { 3, 4, 5 },
        { 6, 7, 8 }
    };
    private int MaxFringeSize = 0;
    private int NodesExpanded = 0;

    public void Solve(State initialState)
    {
        var visited = new HashSet<Board>();
        var queue = new Queue<State>();

        queue.Enqueue(initialState);
        visited.Add(initialState.CurrentBoard);

        while (queue.Count > 0)
        {
            // Track the maximum size of the fringe
            if (queue.Count > MaxFringeSize)
            {
                MaxFringeSize = queue.Count;
            }

            var state = queue.Dequeue();

            // Check if the current state is the goal
            if (state.CurrentBoard.IsEqual(GoalState))
            {
                PrintResults(state);
                return;
            }

            NodesExpanded++;

            // Generate and add child states to the queue
            var children = GenerateChildrenStates(state);
            foreach (var child in children)
            {
                if (!visited.Contains(child.CurrentBoard))
                {
                    queue.Enqueue(child);
                    visited.Add(child.CurrentBoard);
                }
            }
        }
    }

    private static List<State> GenerateChildrenStates(State currentState)
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


    private void PrintResults(State finalState)
    {
        var path = FindPath(finalState);
        path.Reverse(); // Reverse the path to get it from the initial state to the final state

        Console.WriteLine("Solution Moves:");
        foreach (var move in path)
        {
            Console.WriteLine(move);
        }

        var costOfPath = path.Count;

        Console.WriteLine($"Cost of path: {costOfPath}");
        Console.WriteLine($"Nodes expanded: {NodesExpanded}");
        Console.WriteLine($"Max fringe size: {MaxFringeSize}");
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
