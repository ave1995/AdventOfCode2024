using System.Diagnostics;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Extensions;

[PSerializable]
public class MeasureExecutionTimeAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        // Record the start time when method is entered
        args.MethodExecutionTag = Stopwatch.StartNew();
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        // Measure the time when the method exits
        var stopwatch = (Stopwatch)args.MethodExecutionTag;
        stopwatch.Stop();

        // Log the time taken to execute the method
        Console.WriteLine($"Method {args.Method.Name} took {stopwatch.ElapsedMilliseconds} ms to execute.");
    }
}