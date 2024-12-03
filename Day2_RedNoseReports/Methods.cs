using Extensions;

namespace Day2;

public static class Methods
{
    private static IEnumerable<int[]> ParseInput(string input)
    {
        var rows = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (var row in rows)
            yield return row.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }

    private static bool IsSafe(int[] array)
    {
        if (array.Length < 2) return false;

        var prevDifference = array[1] - array[0];

        if (prevDifference == 0 || Math.Abs(prevDifference) > 3) return false;

        var isIncreasing = prevDifference > 0;

        for (var i = 2; i < array.Length; i++)
        {
            var currentDifference = array[i] - array[i - 1];

            if (currentDifference == 0 || Math.Abs(currentDifference) > 3) return false;

            if (currentDifference > 0 != isIncreasing) return false;
        }

        return true;
    }
    
    private static bool IsSafeWithOneLife(int[] array)
    {
        if (IsSafe(array))  return true;

        for (int i = 0; i < array.Length; i++)
        {
            var temp = array.Where((_, index) => index != i).ToArray();
            if (!IsSafe(temp)) continue;
            return true;
        }

        return false;
    }

    [MeasureExecutionTimeAspect]
    public static int SafeReportsCountWithOneError(string input)
    {
        var arrays = ParseInput(input);
        return arrays.Count(IsSafeWithOneLife);
    }

    [MeasureExecutionTimeAspect]
    public static int SafeReportsCount(string input)
    {
        var arrays = ParseInput(input);
        return arrays.Count(IsSafe);
    }
}