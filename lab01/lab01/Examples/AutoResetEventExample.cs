namespace lab01.Examples;

public class AutoResetEventExample : IExample
{
    private static readonly AutoResetEvent WaitHandle = new(false);

    public void Run()
    {
        Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");

        var t = new Thread(Add);
        t.Start(new Params(10, 10));

        WaitHandle.WaitOne();
        Console.WriteLine("All threads finished.");
    }

    static void Add(object? obj)
    {
        if (obj is Params p)
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"{p.A} + {p.B} = {p.A + p.B}");
            WaitHandle.Set();
        }
    }

    private class Params(int a, int b)
    {
        public int A = a, B = b;
    }
}