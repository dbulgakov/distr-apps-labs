namespace lab01.Examples;

public sealed class ParameterizedThread : IExample
{
    private sealed class Params(int a, int b)
    {
        public int A { get; } = a;
        public int B { get; } = b;
    }

    public Task RunAsync(CancellationToken ct = default)
    {
        Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");
        var pm = new Params(10, 10);
        var t = new Thread(Add);
        t.Start(pm);
        t.Join();
        return Task.CompletedTask;
    }

    private static void Add(object? obj)
    {
        if (obj is Params pr)
        {
            Console.WriteLine($"Thread ID in Add(): {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"{pr.A} + {pr.B} = {pr.A + pr.B}");
        }
    }
}