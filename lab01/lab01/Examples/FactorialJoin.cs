namespace lab01.Examples;

public sealed class FactorialJoin : IExample
{
    private static long Factorial(long n)
    {
        long res = 1;
        do
        {
            res *= n;
        } while (--n > 0);
        return res;
    }

    public Task RunAsync(CancellationToken ct = default)
    {
        const long n1 = 5000;
        const long n2 = 10000;
        long res1 = 0, res2 = 0;

        var t1 = new Thread(() => { res1 = Factorial(n1); });
        var t2 = new Thread(() => { res2 = Factorial(n2); });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        Console.WriteLine($"Factorial of {n1} equals {res1}");
        Console.WriteLine($"Factorial of {n2} equals {res2}");

        return Task.CompletedTask;
    }
}