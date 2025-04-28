namespace lab01.Examples;

public class ThreadPropertiesExample : IExample
{
    public void Run()
    {
        var t = Thread.CurrentThread;
        t.Name = "MainThread";

        foreach (var prop in t.GetType().GetProperties())
        {
            Console.WriteLine($"{prop.Name}: {prop.GetValue(t)}");
        }
    }
}