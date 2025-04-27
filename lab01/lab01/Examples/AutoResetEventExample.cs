namespace lab01.Examples;

public sealed class AutoResetEventExample : IExample
{
    private static readonly AutoResetEvent _waitHandle = new(false);

    public Task RunAsync(CancellationToken ct = default)
    {
        Console.WriteLine($"Main thread ID: {Environment.CurrentManagedThreadId}");

        var t = new Thread(Add);
        var parameters = new Params(10, 10);
        t.Start(parameters);

        _waitHandle.WaitOne();

        Console.WriteLine("All threads finished");
        return Task.CompletedTask;
    }

    private static void Add(object? obj)
    {
        if (obj is Params p)
        {
            Console.WriteLine($"Worker thread ID: {Environment.CurrentManagedThreadId}");
            Console.WriteLine($"{p.A} + {p.B} = {p.A + p.B}");
            _waitHandle.Set();
        }
    }

    private sealed record Params(int A, int B);
}