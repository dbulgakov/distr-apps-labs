namespace lab01.Examples;

public class MainThreadChildThreadExample : IExample
{
    public void Run()
    {
        var thread = new Thread(ThreadFunction);
        thread.Start();

        for (var i = 0; i < 3; i++)
        {
            Console.WriteLine("This is the main thread!");
        }
        Console.ReadLine();
    }

    private static void ThreadFunction()
    {
        for (var i = 0; i < 3; i++)
        {
            Console.WriteLine("This is the child thread!");
        }
    }
}