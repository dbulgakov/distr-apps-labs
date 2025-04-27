namespace lab01.Examples;

public sealed class JoinThreads : IExample
{
    public Task RunAsync(CancellationToken ct = default)
    {
        var thr1 = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
                Console.Write("A");
        });

        var thr2 = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
                Console.Write("B");
        });

        var thr3 = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
                Console.Write("C");
        });

        thr1.Start();
        thr2.Start();
        thr1.Join();
        thr2.Join();
        thr3.Start();
        thr3.Join();

        Console.WriteLine();
        return Task.CompletedTask;
    }
}