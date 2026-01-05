public sealed class LoggerService
{
    private static readonly Lazy<LoggerService> _instance =
        new Lazy<LoggerService>(() => new LoggerService());

    private readonly Queue<LogEntry> _logQueue;
    private readonly string _logFilePath;

    private LoggerService()
    {
        _logQueue = new Queue<LogEntry>();
        _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "app.log");
        Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath));
    }

    public static LoggerService Instance => _instance.Value;

    public void LogInfo(string message)
    {
        Log(LogLevel.Info, message);
    }

    public void LogError(string message, Exception ex = null)
    {
        Log(LogLevel.Error, message, ex);
    }

    public void LogWarning(string message)
    {
        Log(LogLevel.Warning, message);
    }

    private void Log(LogLevel level, string message, Exception ex = null)
    {
        var entry = new LogEntry
        {
            Timestamp = DateTime.Now,
            Level = level,
            Message = message,
            Exception = ex?.ToString()
        };

        _logQueue.Enqueue(entry);
        WriteToFile(entry);
    }

    private void WriteToFile(LogEntry entry)
    {
        var logMessage = $"[{entry.Timestamp:yyyy-MM-dd HH:mm:ss}] [{entry.Level}] {entry.Message}";
        if (!string.IsNullOrEmpty(entry.Exception))
        {
            logMessage += $"\nException: {entry.Exception}";
        }

        File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        Console.WriteLine(logMessage);
    }

    public IEnumerable<LogEntry> GetRecentLogs(int count = 10)
    {
        return _logQueue.TakeLast(count);
    }
}

public enum LogLevel
{
    Info,
    Warning,
    Error
}

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public LogLevel Level { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }
}

public class UserService
{
    public void CreateUser(string username)
    {
        LoggerService.Instance.LogInfo($"Creating user: {username}");
        LoggerService.Instance.LogInfo($"User {username} created successfully");
    }
}

public class PaymentService
{
    public void ProcessPayment(decimal amount)
    {
        try
        {
            LoggerService.Instance.LogInfo($"Processing payment: {amount:C}");
            throw new Exception("Payment gateway timeout");
        }
        catch (Exception ex)
        {
            LoggerService.Instance.LogError($"Payment failed for amount {amount:C}", ex);
        }
    }
}