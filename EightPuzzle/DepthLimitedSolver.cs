namespace EightPuzzle;
public class DepthLimitedSolver : Solver
{
    private readonly int depthLimit;

    public DepthLimitedSolver(int depthLimit)
    {
        this.depthLimit = depthLimit;
    }

    public override void Solve(State initialState, bool printResults = true)
    {
        var visited = new HashSet<Board>();
        var stack = new Stack<State>();

        stack.Push(initialState);
        visited.Add(initialState.CurrentBoard);

        while (stack.Count > 0)
        {
            if (stack.Count > MaxFringeSize)
            {
                MaxFringeSize = stack.Count;
            }

            var state = stack.Pop();

            if (state.SearchDepth > MaxSearchDepth)
            {
                MaxSearchDepth = state.SearchDepth;
            }

            // If the current state's depth exceeds the depth limit skip processing it
            if (state.SearchDepth > depthLimit)
            {
                continue;
            }

            // Check if the current state is the goal
            if (state.CurrentBoard.IsEqual(GoalState))
            {
                if (printResults)
                {
                    PrintResults(state);
                }
                return;
            }

            NodesExpanded++;

            // Generate child states and push to the stack
            var children = GenerateChildrenStates(state);
            foreach (var child in children)
            {
                // if within the depth limit
                if (!visited.Contains(child.CurrentBoard) && child.SearchDepth <= depthLimit)
                {
                    stack.Push(child);
                    visited.Add(child.CurrentBoard);
                }
            }
        }

        Console.WriteLine($"No solution found within the depth limit of {depthLimit}.");
    }
}
