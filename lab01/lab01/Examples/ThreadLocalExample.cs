namespace lab01.Examples;

public sealed class ThreadLocalExample : IExample
{
    public Task RunAsync(CancellationToken ct = default)
    {
        ThreadLocal<int> localSum = new(() => 0);

        var t1 = new Thread(() =>
        {
            for (var i = 0; i < 10; i++)
                localSum.Value++;
            Console.WriteLine(localSum.Value);
        });

        var t2 = new Thread(() =>
        {
            for (var i = 0; i < 10; i++)
                localSum.Value--;
            Console.WriteLine(localSum.Value);
        });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        Console.WriteLine(localSum.Value);
        return Task.CompletedTask;
    }
}