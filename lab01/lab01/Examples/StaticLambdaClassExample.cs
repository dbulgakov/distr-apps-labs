namespace lab01.Examples;

public class StaticLambdaClassExample : IExample
{
    public void Run()
    {
        var thr1 = new Thread(LocalWorkItem);
        thr1.Start();

        var thr2 = new Thread(() => Console.WriteLine("Hello from lambda expression"));
        thr2.Start();

        var threadClass = new ThreadClass("Hello from thread class");
        var thr3 = new Thread(threadClass.Run);
        thr3.Start();

        thr1.Join();
        thr2.Join();
        thr3.Join();
    }

    static void LocalWorkItem()
    {
        Console.WriteLine("Hello from static method");
    }

    private class ThreadClass(string greeting)
    {
        public void Run() => Console.WriteLine(greeting);
    }
}