using lab01.Examples;

var examples = new IExample[]
{
    new ThreadNumbersExample(),
    new StaticLambdaClassExample(),
    new MainThreadChildThreadExample(),
    new JoinThreadsExample(),
    new PassParametersGlobalVarsExample(),
    new PassParametersStartExample(),
    new WrongLambdaExample(),
    new CorrectLambdaExample(),
    new SleepExample(),
    new ParametrizedThreadStartExample(),
    new AutoResetEventExample(),
    new ThreadPropertiesExample(),
    new ThreadPriorityExample(),
    new ForegroundBackgroundThreadsExample(),
    new ThreadLocalStaticExample(),
    new ThreadLocalClassExample(),
    new NamedDataSlotExample()
};

foreach (var example in examples)
{
    Console.WriteLine($"\nRunning: {example.GetType().Name}");
    example.Run();
    Console.WriteLine("Finished.\n---------------------------");
}