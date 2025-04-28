namespace lab01.Examples;

public class NamedDataSlotExample : IExample
{
    public void Run()
    {
        var worker = new ThreadWork();

        new Thread(() => worker.Run("one")).Start();
        new Thread(() => worker.Run("two")).Start();
        new Thread(() => worker.Run("three")).Start();

        Thread.Sleep(1000);
    }

    private class ThreadWork
    {
        private string _sharedWord = "";

        public void Run(string secretWord)
        {
            _sharedWord = secretWord;
            Save(secretWord);
            Thread.Sleep(500);
            Show();
        }

        private void Save(string s)
        {
            var slot = Thread.GetNamedDataSlot("Secret");
            Thread.SetData(slot, s);
        }

        private void Show()
        {
            var slot = Thread.GetNamedDataSlot("Secret");
            var secretWord = Thread.GetData(slot) as string;
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: secret word = {secretWord}, shared word = {_sharedWord}");
        }
    }
}