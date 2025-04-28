namespace lab01.Examples;

public class ThreadPriorityExample : IExample
{
    private static long[] _counts = new long[5];
    private static bool _finish;

    public void Run()
    {
        var threads = new Thread[5];

        for (var i = 0; i < threads.Length; i++)
        {
            var index = i;
            threads[i] = new Thread(() => ThreadFunc(index))
            {
                Priority = (ThreadPriority)index
            };
        }

        foreach (var t in threads)
            t.Start();

        Thread.Sleep(5000);
        _finish = true;

        foreach (var t in threads)
            t.Join();

        for (int i = 0; i < threads.Length; i++)
            Console.WriteLine($"Thread with priority {(ThreadPriority)i}: Counts = {_counts[i]}");
    }

    static void ThreadFunc(int index)
    {
        while (!_finish)
        {
            _counts[index]++;
        }
    }
}