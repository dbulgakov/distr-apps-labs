namespace lab01.Examples;

public class SleepExample : IExample
{
    public void Run()
    {
        var threads = new Thread[4];
        var names = new[] { "A", "B", "C", "D" };

        for (var i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(ThreadFunc);
            threads[i].Start(names[i]);
        }

        foreach (var t in threads)
            t.Join();
    }

    private static void ThreadFunc(object? o)
    {
        for (var i = 0; i < 20; i++)
        {
            Console.Write(o);
            Thread.Sleep(0);
        }
    }
}