namespace EightPuzzle;
public class ProblemGenerator
{
    private readonly Random _random = new();
    private static readonly int[,] GoalState = new int[3, 3]
    {
        { 0, 1, 2 },
        { 3, 4, 5 },
        { 6, 7, 8 }
    };

    public Board GenerateRandomBoard()
    {
        int[,] tiles;

        do
        {
            tiles = ShuffleTiles();
        } while (!IsSolvable(tiles));

        return new Board(tiles);
    }

    private int[,] ShuffleTiles()
    {
        List<int> tileList = [0, 1, 2, 3, 4, 5, 6, 7, 8];
        for (int i = tileList.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (tileList[i], tileList[j]) = (tileList[j], tileList[i]);
        }

        int[,] tiles = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tiles[i, j] = tileList[i * 3 + j];
            }
        }
        return tiles;
    }

    private static bool IsSolvable(int[,] tiles)
    {
        List<int> tileList = new();
        foreach (int tile in tiles)
        {
            if (tile != 0)
            {
                tileList.Add(tile);
            }
        }

        int inversions = 0;
        for (int i = 0; i < tileList.Count - 1; i++)
        {
            for (int j = i + 1; j < tileList.Count; j++)
            {
                if (tileList[i] > tileList[j])
                {
                    inversions++;
                }
            }
        }

        return inversions % 2 == 0;
    }
}
