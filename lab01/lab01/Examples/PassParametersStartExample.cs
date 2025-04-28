namespace lab01.Examples;

public class PassParametersStartExample : IExample
{
    private static double _result;

    public void Run()
    {
        var thread = new Thread(ThreadWork);
        thread.Start(new object[] { "Thread #1", 3.14 });
        thread.Join();

        Console.WriteLine($"Result: {_result}");
    }

    static void ThreadWork(object? state)
    {
        if (state is object[] arr)
        {
            var title = arr[0] as string ?? "";
            var value = (double)arr[1];
            Console.WriteLine(title);
            _result = SomeMathOperation(value);
        }
    }

    static double SomeMathOperation(double d) => d * 2;
}