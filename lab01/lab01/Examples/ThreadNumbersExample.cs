namespace lab01.Examples;

public class ThreadNumbersExample : IExample
{
    public void Run()
    {
        Console.Write("How many threads to use (1 or 2)? ");
        var number = Console.ReadLine();

        var current = Thread.CurrentThread;
        current.Name = "Primary";

        Console.WriteLine($"--> {current.Name} main thread");

        var myThread = new MyThread();

        switch (number)
        {
            case "1":
                myThread.ThreadNumbers();
                break;
            case "2":
                var backgroundThread = new Thread(myThread.ThreadNumbers)
                {
                    Name = "Secondary"
                };
                backgroundThread.Start();
                backgroundThread.Join();
                break;
            default:
                Console.WriteLine("Using 1 thread by default");
                myThread.ThreadNumbers();
                break;
        }
    }

    private class MyThread
    {
        public void ThreadNumbers()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is executing ThreadNumbers method");
            Console.Write("Numbers: ");
            for (var i = 0; i < 10; i++)
            {
                Console.Write($"{i}, ");
                Thread.Sleep(300);
            }
            Console.WriteLine();
        }
    }
}