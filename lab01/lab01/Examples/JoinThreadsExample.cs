namespace lab01.Examples;

public class JoinThreadsExample : IExample
{
    public void Run()
    {
        var thr1 = new Thread(() => { for (var i = 0; i < 5; i++) Console.Write("A"); });
        var thr2 = new Thread(() => { for (var i = 0; i < 5; i++) Console.Write("B"); });
        var thr3 = new Thread(() => { for (var i = 0; i < 5; i++) Console.Write("C"); });

        thr1.Start();
        thr2.Start();
        thr1.Join();
        thr2.Join();
        thr3.Start();
        thr3.Join();
    }
}