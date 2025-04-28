namespace lab01.Examples;

public class ThreadLocalStaticExample : IExample
{
    public void Run()
    {
        Thread t1 = new(ThreadFunc);
        Thread t2 = new(ThreadFunc);

        Data.SharedVar = 3;
        Data.LocalVar = 3;

        t1.Start(1);
        t2.Start(2);

        t1.Join();
        t2.Join();
    }

    static void ThreadFunc(object? i)
    {
        Console.WriteLine($"Thread {i}: Before - Shared: {Data.SharedVar}, Local: {Data.LocalVar}");
        Data.SharedVar = (int)(i ?? 0);
        Data.LocalVar = (int)(i ?? 0);
        Console.WriteLine($"Thread {i}: After - Shared: {Data.SharedVar}, Local: {Data.LocalVar}");
    }

    private static class Data
    {
        public static int SharedVar;
        [ThreadStatic] public static int LocalVar;
    }
}