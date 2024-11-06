namespace EightPuzzle;
public class IterativeDeepeningSolver : Solver
{
    public override void Solve(State initialState, bool printResults = true)
    {
        int depth = 0;
        while (true)
        {
            var result = DepthLimitedSearch(initialState, depth);

            if (result is not null)
            {
                if (printResults)
                {
                    PrintResults(result);
                }
                return;
            }

            depth++;
        }
    }

    private State? DepthLimitedSearch(State initialState, int depthLimit)
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
                return state;
            }

            NodesExpanded++;

            // Generate child states and push to the stack
            var children = GenerateChildrenStates(state);
            foreach (var child in children)
            {
                // if within the depth limit
                if (!visited.Contains(child.CurrentBoard))
                {
                    stack.Push(child);
                    visited.Add(child.CurrentBoard);
                }
            }
        }

        // Return null if no solution is found within the current depth limit
        return null;
    }
}