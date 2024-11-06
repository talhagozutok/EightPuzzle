using System.Text;

namespace EightPuzzle;

public class BfsSolver : Solver
{
    public override void Solve(State initialState, bool printResults = true)
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
                if (printResults)
                {
                    PrintResults(state);
                }
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
}
