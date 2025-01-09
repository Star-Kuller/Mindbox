using SQLRequest.Interfaces;

namespace SQLRequest;

public class ConsoleLogger(bool enable = true) : ILogger
{
    public void Log(string message)
    {
        if(enable)
            Console.WriteLine(message);
    }
}