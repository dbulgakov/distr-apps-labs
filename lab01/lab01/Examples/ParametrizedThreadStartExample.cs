namespace lab01.Examples;

public class ParametrizedThreadStartExample : IExample
{
    public void Run()
    {
        Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");

        var pm = new Params(10, 10);
        var t = new Thread(Add);
        t.Start(pm);

        t.Join();
    }

    static void Add(object? obj)
    {
        if (obj is Params p)
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"{p.A} + {p.B} = {p.A + p.B}");
        }
    }

    private class Params(int a, int b)
    {
        public int A = a, B = b;
    }
}