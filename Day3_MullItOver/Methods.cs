using System.Text.RegularExpressions;
using Extensions;

namespace Day3;

public static partial class Methods
{
    [MeasureExecutionTimeAspect]
    public static int MullItOver(string input)
    {
        var matches = MullRegex().Matches(input);

        var result = 0;
        
        foreach (Match match in matches)
        {
            result += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
        }
        
        return result;
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MullRegex();
}