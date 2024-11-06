using EightPuzzle;

namespace EightPuzzle;

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
}