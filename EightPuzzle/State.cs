namespace EightPuzzle;
public class State
{
    public Board CurrentBoard { get; set; }
    public State? Parent { get; set; }
    public string LastMove { get; set; }
    public int SearchDepth { get; set; }

    public State(Board currentBoard, State? parent, string lastMove, int searchDepth)
    {
        CurrentBoard = currentBoard;
        Parent = parent;
        LastMove = lastMove;
        SearchDepth = searchDepth;
    }

    // Move the empty tile
    public State? MoveZero(int dx, int dy)
    {
        var (zeroX, zeroY) = CurrentBoard.IndexOfZero();
        int newX = zeroX + dx, newY = zeroY + dy;

        if (newX < 0 || newX >= 3 || newY < 0 || newY >= 3)
            return null; // Out of bounds

        var newTiles = (int[,])CurrentBoard.Tiles.Clone();
        (newTiles[zeroX, zeroY], newTiles[newX, newY]) = (newTiles[newX, newY], newTiles[zeroX, zeroY]);

        return new State(
            currentBoard: new Board(newTiles),
            parent: this,
            lastMove: $"Move {dx},{dy}",
            SearchDepth + 1);
    }

    // Override Equals and GetHashCode for comparisons in collections
    public override bool Equals(object obj)
    {
        return obj is State other && CurrentBoard.Equals(other.CurrentBoard);
    }
    public override int GetHashCode()
    {
        return CurrentBoard.GetHashCode();
    }
}