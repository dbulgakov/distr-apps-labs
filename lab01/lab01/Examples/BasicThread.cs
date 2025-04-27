namespace lab01.Examples;

public sealed class BasicThread : IExample
{
    public Task RunAsync(CancellationToken ct = default)
    {
        var primary = Thread.CurrentThread;
        primary.Name = "Primary";

        var workerObj = new MyThread();
        var secondary = new Thread(workerObj.ThreadNumbers)
        {
            Name = "Secondary"
        };
        secondary.Start();
        secondary.Join();

        return Task.CompletedTask;
    }

    private sealed class MyThread
    {
        public void ThreadNumbers()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is running ThreadNumbers method");
            Console.Write("Numbers: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + ", ");
                Thread.Sleep(3000);
            }
            Console.WriteLine();
        }
    }
}