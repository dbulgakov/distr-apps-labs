namespace lab01.Examples;

public sealed class LambdaThread : IExample
{
    public Task RunAsync(CancellationToken ct = default)
    {
        var thr1 = new Thread(LocalWorkItem);
        thr1.Start();

        var thr2 = new Thread(() =>
        {
            Console.WriteLine("Hello from lambda expression");
        });
        thr2.Start();

        var thrClass = new ThreadClass("Hello from thread class");
        var thr3 = new Thread(thrClass.Run);
        thr3.Start();

        thr1.Join();
        thr2.Join();
        thr3.Join();
        return Task.CompletedTask;
    }

    private static void LocalWorkItem()
    {
        Console.WriteLine("Hello from static method");
    }

    private sealed class ThreadClass(string greeting)
    {
        public void Run() => Console.WriteLine(greeting);
    }
}