using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public abstract void Solve(State initialState);

    protected abstract List<State> GenerateChildrenStates(State currentState);

    protected void PrintResults(State finalState)
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