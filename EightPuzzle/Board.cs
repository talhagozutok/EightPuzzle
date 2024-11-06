namespace EightPuzzle;

public class Board(int[,] tiles)
{
    public int[,] Tiles { get; set; } = (int[,])tiles.Clone();

    // Find the index of zero (ie. empty tile)
    public (int, int) IndexOfZero()
    {
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                if (Tiles[i, j] == 0)
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1); // If there is no zero in the board
    }

    // Check equality of two boards
    public bool IsEqual(int[,] tilesToTest)
    {
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                if (Tiles[i, j] != tilesToTest[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Generate a unique hash code for the board state
    public override int GetHashCode()
    {
        int hash = 0;
        int multiplier = 1;

        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                hash += Tiles[i, j] * multiplier;
                multiplier *= 10;
            }
        }
        return hash;
    }

    // Check if this board is equal to another board
    public override bool Equals(object obj)
    {
        if (obj is not Board otherBoard)
        {
            return false;
        }

        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                if (Tiles[i, j] != otherBoard.Tiles[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
