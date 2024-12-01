using Extensions;

namespace Day1;

public static class Methods
{
    [MeasureExecutionTimeAspect]
    public static int CountTotalDistance(string input, bool log = false)
    {
        var (column1, column2) = ParseInput(input).OrderArray();

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

    //26674158
    [MeasureExecutionTimeAspect]
    public static int CountSimilarityScore(string input)
    {
        var (array, length) = ParseInput(input);
        var dictionary = new Dictionary<int, int>();
        
        var totalSimilarity = 0;

        for (var i = 0; i < length; i++)
        {
            var value = array[i, 1];
            if(!dictionary.TryAdd(value, 1))
                dictionary[value]++;
        }

        for (var i = 0; i < length; i++)
        {
            var value = array[i, 0];
            totalSimilarity += dictionary.TryGetValue(value, out var occurence) ? occurence * value : 0;
        }

        return totalSimilarity;
    }
    
    private static (int[,] array2X, int length) ParseInput(string input)
    {
        var rows = input.Split(['\n'], StringSplitOptions.RemoveEmptyEntries);

        var array = new int[rows.Length, 2];

        for (var i = 0; i < rows.Length; i++)
        {
            var columns = rows[i].Split([' '], StringSplitOptions.RemoveEmptyEntries);
            array[i, 0] = int.Parse(columns[0]);
            array[i, 1] = int.Parse(columns[1]);
        }
        return (array, rows.Length);
    }

    private static (int[] column1, int[] column2) OrderArray(this (int[,] array, int length) tuple)
    {
        var column1 = Enumerable.Range(0, tuple.length)
            .Select(i => tuple.array[i, 0])
            .OrderBy(i => i)
            .ToArray();

        var column2 = Enumerable.Range(0, tuple.length)
            .Select(i => tuple.array[i, 1])
            .OrderBy(i => i)
            .ToArray();

        return (column1, column2);
    }
}