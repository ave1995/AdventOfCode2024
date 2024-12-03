using System.Text;
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

    [MeasureExecutionTimeAspect]
    public static int MullItOverWithDo(string input)
    {
        var doStrings = ParseStringWithDo(input);
        
        var matches = MullRegex().Matches(doStrings);
        
        var result = 0;

        foreach (Match match in matches)
        {
            result += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
        }

        return result;
    }
    
    private static string ParseStringWithDo(string input)
    {
        StringBuilder sb = new();
        int currentIndex = 0;

        string[] patterns = ["don't()", "do()"];

        int firstOccurenceDontIndex = input.IndexOf(patterns[0], currentIndex, StringComparison.Ordinal);
        int firstOccurenceDoIndex = input.IndexOf(patterns[1], currentIndex, StringComparison.Ordinal);

        int smallestFirstOccurenceIndex = Math.Min(firstOccurenceDontIndex, firstOccurenceDoIndex);
        
        string text = input[..smallestFirstOccurenceIndex];
        
        sb.Append(text);
        
        currentIndex = smallestFirstOccurenceIndex;

        while (currentIndex < input.Length)
        {
            int nextOccurenceDontIndex = input.IndexOf(patterns[0], currentIndex, StringComparison.Ordinal);
            int nextOccurenceDoIndex = input.IndexOf(patterns[1], currentIndex, StringComparison.Ordinal);

            if (nextOccurenceDoIndex < nextOccurenceDontIndex)
            {
                string nextTest = input.Substring(nextOccurenceDoIndex, nextOccurenceDontIndex - nextOccurenceDoIndex);
                sb.Append(nextTest);
                currentIndex = nextOccurenceDontIndex;
            }
            else if (nextOccurenceDontIndex == -1)
            {
                string nextTest = input[nextOccurenceDoIndex..];
                sb.Append(nextTest);
                break;
            }
            else
            {
                currentIndex = nextOccurenceDoIndex;
            }
            
        }

        return sb.ToString();
    }
}