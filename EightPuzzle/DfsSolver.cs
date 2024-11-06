using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightPuzzle;
public class DfsSolver
{
    private readonly int[,] GoalState = new int[3, 3]
    {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 }
    };

    private int MaxFringeSize = 0;
    private int NodesExpanded = 0;
    private int MaxSearchDepth = 0;

    public void Solve(State initialState)
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

            // Check if the current state is the goal
            if (state.CurrentBoard.IsEqual(GoalState))
            {
                PrintResults(state);
                return;
            }

            NodesExpanded++;

            // Generate child states and push to the stack
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

    private List<State> GenerateChildrenStates(State currentState)
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

    private void PrintResults(State finalState)
    {
        var path = FindPath(finalState);
        path.Reverse(); // Reverse the path to get it from the initial state to the final state

        Console.WriteLine("Solution Moves:");
        foreach (var move in path)
        {
            Console.Write(move + " ");
        }

        var costOfPath = path.Count;

        Console.WriteLine($"\nCost of path: {costOfPath}");
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