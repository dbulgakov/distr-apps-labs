namespace lab01.Examples;

public class ForegroundBackgroundThreadsExample : IExample
{
    public void Run()
    {
        var backgroundThread = new Thread(() => Console.WriteLine("Background thread working"))
        {
            IsBackground = true
        };
        backgroundThread.Start();
    }
}