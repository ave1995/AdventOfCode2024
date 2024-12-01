using Extensions;

namespace Day1;

public static class Methods
{
    public static int CountTotalDistance(string input, bool log = false)
    {
        var (column1, column2) = ParseInput(input);

        var totalCount = 0;

        for (int i = 0; i < column2.Length; i++)
        {
            if (log)
                Console.WriteLine($"{column2[i]} - {column1[i]}");
            totalCount += Math.Abs(column2[i] - column1[i]);
        }

        return totalCount;
        //return column2.Select((t, i) => Math.Abs(column1[i] - t)).Sum();
    }

    [MeasureExecutionTimeAspect]
    public static int CountSimilarityScore(string input, bool log = false)
    {
        var (column1, column2) = ParseInput(input);

        var totalSimilarity = 0;
        var ignoredIndex = 0;
        
        
        foreach (var column1Value in column1)
        {
            var firstOccurence = BinarySearchFirstOccurence(column2.Skip(ignoredIndex).ToArray(), column1Value);
            var lastOccurence = BinarySearchLastOccurence(column2.Skip(ignoredIndex).ToArray(), column1Value);
            
            if (firstOccurence == -1 || lastOccurence == -1) continue;
            
            var occurence = lastOccurence - firstOccurence + 1;
            totalSimilarity += occurence * column1Value;
            ignoredIndex = lastOccurence + 1;
        }

        return totalSimilarity;
    }

    private static (int[] column1, int[] column2) ParseInput(string input)
    {
        var rows = input.Split(['\n'], StringSplitOptions.RemoveEmptyEntries);

        var array = new int[rows.Length, 2];

        for (int i = 0; i < rows.Length; i++)
        {
            var columns = rows[i].Split([' '], StringSplitOptions.RemoveEmptyEntries);
            array[i, 0] = int.Parse(columns[0]);
            array[i, 1] = int.Parse(columns[1]);
        }

        var column1 = Enumerable.Range(0, array.GetLength(0))
            .Select(i => array[i, 0])
            .OrderBy(i => i)
            .ToArray();

        var column2 = Enumerable.Range(0, array.GetLength(0))
            .Select(i => array[i, 1])
            .OrderBy(i => i)
            .ToArray();

        return (column1, column2);
    }

    private static int BinarySearchFirstOccurence(int[] array, int target)
    {
        int low = 0;
        int high = array.Length - 1;
        int result = -1;

        while (low <= high)
        {
            int middle = low + (high - low) / 2;

            if (array[middle] == target)
            {
                result = middle;
                high = middle - 1; // Move left to find the first occurrence
            }
            else if (array[middle] < target)
                low = middle + 1;
            else
                high = middle - 1;
        }

        return result;
    }

    private static int BinarySearchLastOccurence(int[] array, int target)
    {
        int low = 0;
        int high = array.Length - 1;
        int result = -1;

        while (low <= high)
        {
            int middle = low + (high - low) / 2;

            if (array[middle] == target)
            {
                result = middle;
                low = middle + 1; // Move right to find the last occurrence
            }
            else if (array[middle] < target)
                low = middle + 1;
            else
                high = middle - 1;
        }

        return result;
    }
}