namespace lab01.Examples;

public class WrongLambdaExample : IExample
{
    public void Run()
    {
        for (var i = 0; i < 10; i++)
        {
            var t = new Thread(() => Console.Write("ABCDEFGHIJK"[i]));
            t.Start();
        }
    }
}