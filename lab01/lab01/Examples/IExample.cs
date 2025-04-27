namespace lab01.Examples;

public interface IExample
{
    Task RunAsync(CancellationToken cancellationToken = default);
}