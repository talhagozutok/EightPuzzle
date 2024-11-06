using System.Text;

namespace EightPuzzle;

public class BfsSolver : Solver
{
    public override void Solve(State initialState)
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

    protected override List<State> GenerateChildrenStates(State currentState)
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
}
