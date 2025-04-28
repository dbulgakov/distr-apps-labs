namespace lab01.Examples;

public class CorrectLambdaExample : IExample
{
    public void Run()
    {
        for (var i = 0; i < 10; i++)
        {
            var iCopy = i;
            var t = new Thread(() => Console.Write("ABCDEFGHIJK"[iCopy]));
            t.Start();
        }
    }
}