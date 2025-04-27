using lab01.Examples;

while (true)
{
    Console.Clear();
    Console.WriteLine("Available examples:");
    Console.WriteLine("1. BasicThread");
    Console.WriteLine("2. LambdaThread");
    Console.WriteLine("3. JoinThreads");
    Console.WriteLine("4. FactorialJoin");
    Console.WriteLine("5. ParameterizedThread");
    Console.WriteLine("6. AutoResetEventExample");
    Console.WriteLine("7. PriorityExample");
    Console.WriteLine("8. ThreadLocalExample");
    Console.WriteLine("0. Exit");

    Console.Write("\nSelect an example number: ");
    var input = Console.ReadLine();

    if (!int.TryParse(input, out var choice))
    {
        Console.WriteLine("Invalid input. Press Enter to continue...");
        Console.ReadLine();
        continue;
    }

    if (choice == 0)
        break;

    var example = CreateExample(choice);
    if (example == null)
    {
        Console.WriteLine("Invalid choice. Press Enter to continue...");
        Console.ReadLine();
        continue;
    }

    await example.RunAsync();
    Console.WriteLine("\nExample completed. Press Enter to continue...");
    Console.ReadLine();
}


static IExample? CreateExample(int choice) => choice switch
{
    1 => new BasicThread(),
    2 => new LambdaThread(),
    3 => new JoinThreads(),
    4 => new FactorialJoin(),
    5 => new ParameterizedThread(),
    6 => new AutoResetEventExample(),
    7 => new PriorityExample(),
    8 => new ThreadLocalExample(),
    _ => null
};