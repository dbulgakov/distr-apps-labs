namespace lab01.Examples;

public sealed class PriorityExample : IExample
{
    private static readonly long[] Counts = new long[5];
    private static volatile bool _finish;

    private static void ThreadFunc(object? iThreadObj)
    {
        var idx = (int)iThreadObj!;
        while (!_finish)
        {
            Counts[idx]++;
        }
    }

    public Task RunAsync(CancellationToken ct = default)
    {
        var t = new Thread[5];
        for (var i = 0; i < t.Length; i++)
        {
            t[i] = new Thread(ThreadFunc)
            {
                Priority = (ThreadPriority)i,
                IsBackground = true
            };
            t[i].Start(i);
        }

        Thread.Sleep(5000);
        _finish = true;

        foreach (var thr in t)
            thr.Join();

        for (var i = 0; i < t.Length; i++)
            Console.WriteLine($"Thread with priority {(ThreadPriority)i,15}, Counts: {Counts[i]}");

        return Task.CompletedTask;
    }
}