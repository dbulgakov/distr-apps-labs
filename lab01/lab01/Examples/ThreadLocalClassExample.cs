namespace lab01.Examples;

public class ThreadLocalClassExample : IExample
{
    public void Run()
    {
        var localSum = new ThreadLocal<int>(() => 0);

        Thread t1 = new(() =>
        {
            for (int i = 0; i < 10; i++) localSum.Value++;
            Console.WriteLine(localSum.Value);
        });

        Thread t2 = new(() =>
        {
            for (int i = 0; i < 10; i++) localSum.Value--;
            Console.WriteLine(localSum.Value);
        });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        Console.WriteLine(localSum.Value);
    }
}