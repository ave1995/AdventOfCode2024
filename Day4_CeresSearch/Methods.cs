using System.Collections.ObjectModel;

namespace Day4;

public static class Methods
{
    private static readonly (int dx, int dy)[] Directions =
    [
        (0, 1), // Right
        (0, -1), // Left
        (1, 0), // Down
        (-1, 0), // Up
        (1, 1), // Diagonal Down-Right
        (-1, -1), // Diagonal Up-Left
        (1, -1), // Diagonal Down-Left
        (-1, 1) // Diagonal Up-Right
    ];

    private static readonly (int dx, int dy)[] XDirectionsLeftTopToRightBot =
    [
        (-1, -1), // Diagonal Up-Left
        (1, 1) // Diagonal Down-Right
    ];

    private static readonly (int dx, int dy)[] XDirectionsRightTopToLeftBot =
    [
        (-1, 1), // Diagonal Up-Right
        (1, -1), // Diagonal Down-Left
    ];

    private static readonly (int dx, int dy)[] XDirections =
        XDirectionsLeftTopToRightBot.Concat(XDirectionsRightTopToLeftBot).ToArray();


    private static string[] ParseInputIntoRows(string input)
    {
        var rows = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        return rows;
    }

    private static (char[,] array, List<(int x, int y)> xPositions) FindSpecificCharInRows(string[] rows,
        char specificChar)
    {
        char[,] arrayWithX = new char[rows[0].Length, rows.Length];

        var positions = new List<(int x, int y)>();

        int index = 0;

        foreach (var row in rows)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == specificChar)
                    positions.Add((i, index));

                arrayWithX[i, index] = row[i];
            }

            index++;
        }

        return (arrayWithX, positions);
    }

    public static int OccurrencesXmas(string input)
    {
        var rows = ParseInputIntoRows(input);

        var (array, positions) = FindSpecificCharInRows(rows, 'X');

        var targetWord = "XMAS";
        int targetWordLength = targetWord.Length;
        var occurrences = 0;

        foreach (var position in positions)
        {
            foreach (var (dx, dy) in Directions)
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

    public static int OccurrencesMasInShapeX(string input)
    {
        var rows = ParseInputIntoRows(input);
        var (array, positions) = FindSpecificCharInRows(rows, 'A');
        var occurrences = 0;

        foreach (var position in positions)
        {
            if (!CanFormX(array, position.x, position.y)) continue;

            if (IsItX(array, position.x, position.y))
                occurrences++;
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

    private static bool CanFormX(char[,] array, int startX, int startY)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        foreach (var (dx, dy) in XDirections)
        {
            int x = startX + 1 * dx;
            int y = startY + 1 * dy;

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

    //Target word is MAS, i find all A so i look only for M and S
    private static bool IsItX(char[,] array, int startX, int startY)
    {
        HashSet<char> targets = ['M', 'S'];

        HashSet<char> founded = [];

        foreach (var (dx, dy) in XDirectionsLeftTopToRightBot)
        {
            var letter = array[startX + dx, startY + dy];
            if (!targets.Contains(letter))
                return false;

            if (!founded.Add(letter))
                return false;
        }

        founded.Clear();

        foreach (var (dx, dy) in XDirectionsRightTopToLeftBot)
        {
            var letter = array[startX + dx, startY + dy];
            if (!targets.Contains(letter))
                return false;

            if (!founded.Add(letter))
                return false;
        }

        return true;
    }
}