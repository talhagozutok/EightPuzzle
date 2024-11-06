using EightPuzzle;

public class DfsSolver : Solver
{
    public override void Solve(State initialState)
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

            if (state.CurrentBoard.IsEqual(GoalState))
            {
                PrintResults(state);
                return;
            }

            NodesExpanded++;

            var children = GenerateChildrenStates(state);
            foreach (var child in children)
            {
                if (!visited.Contains(child.CurrentBoard))
                {
                    stack.Push(child);
                    visited.Add(child.CurrentBoard);
                }
            }
        }
    }

    protected override List<State> GenerateChildrenStates(State currentState)
    {
        var children = new List<State>();

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
}