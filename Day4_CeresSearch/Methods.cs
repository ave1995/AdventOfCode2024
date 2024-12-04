namespace Day4;

public static class Methods
{
    private static (char[,] array, List<(int x, int y)> xPositions) ParseInputIntoRows(string input)
    {
        var rows = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        char[,] arrayWithX = new char[rows[0].Length, rows.Length];

        var X_Positions = new List<(int x, int y)>();

        int index = 0;

        foreach (var row in rows)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == 'X')
                    X_Positions.Add((i, index));

                arrayWithX[i, index] = row[i];
            }

            index++;
        }

        return (arrayWithX, X_Positions);
    }

    public static int OccurencesXmas(string input)
    {
        var (array, positions) = ParseInputIntoRows(input);

        var targetWord = "XMAS";
        int targetWordLength = targetWord.Length;
        var occurrences = 0;

        var directions = new (int dx, int dy)[]
        {
            (0, 1),   // Right
            (0, -1),  // Left
            (1, 0),   // Down
            (-1, 0),  // Up
            (1, 1),   // Diagonal Down-Right
            (-1, -1), // Diagonal Up-Left
            (1, -1),  // Diagonal Down-Left
            (-1, 1)   // Diagonal Up-Right
        };

        foreach (var position in positions)
        {
            foreach (var (dx, dy) in directions)
            {
                if (CanFormWord(array, position.x, position.y, dx, dy, targetWordLength))
                {
                    string word = BuildWord(array, position.x, position.y, dx, dy, targetWordLength);
                    if (word == targetWord)
                    {
                        occurrences++;
                    }
                }
            }
        }

        return occurrences;
    }
    
    
    private static bool CanFormWord(char[,] array, int startX, int startY, int dx, int dy, int length)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < length; i++)
        {
            int x = startX + i * dx;
            int y = startY + i * dy;

            if (x < 0 || x >= rows || y < 0 || y >= cols)
            {
                return false;
            }
        }

        return true;
    }
    
    private static string BuildWord(char[,] array, int startX, int startY, int dx, int dy, int length)
    {
        var chars = new char[length];

        for (int i = 0; i < length; i++)
        {
            int x = startX + i * dx;
            int y = startY + i * dy;
            chars[i] = array[x, y];
        }

        return new string(chars);
    }
}